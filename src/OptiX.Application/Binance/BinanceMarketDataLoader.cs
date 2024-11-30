using Binance.Net.Clients;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OptiX.Application.Asset;
using OptiX.Application.MarketData;
using OptiX.Application.SignalR;

namespace OptiX.Application.Binance;

public sealed class BinanceMarketDataLoader : BackgroundService
{
    private readonly BinanceSocketClient _binanceSocketClient;
    private readonly IHubContext<MarketDataHub> _hubContext;
    private readonly IServiceProvider _serviceProvider;

    public BinanceMarketDataLoader(IServiceProvider serviceProvider, IHubContext<MarketDataHub> hubContext)
    {
        _binanceSocketClient = new BinanceSocketClient();
        _serviceProvider = serviceProvider;
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var assetService = scope.ServiceProvider.GetRequiredService<IAssetService>();
        
        var assets = await assetService.GetAllAsync();
        var symbolsToSubscribe = assets.Select(asset => asset.Symbol);
        var symbolsAndAssetIds = assets.ToDictionary(key => key.Symbol, value => value.Id);

        if (!symbolsAndAssetIds.Any())
            return;
        
        var tickerSubscriptionResult = await _binanceSocketClient.SpotApi.ExchangeData.SubscribeToTradeUpdatesAsync(
            symbolsToSubscribe, async update =>
            {
                using var scope = _serviceProvider.CreateScope();
                var ticksService = scope.ServiceProvider.GetRequiredService<ITicksService>();
                
                var assetId = symbolsAndAssetIds[update.Symbol];
                var tick = new TickDto
                {
                    DateTime = update.Data.TradeTime,
                    Volume = update.Data.Quantity,
                    Price = update.Data.Price
                };

                await ticksService.SaveAsync(assetId, [tick]);
                
                await _hubContext.Clients.Group(update.Symbol).SendAsync("ReceiveMarketData", tick, cancellationToken: stoppingToken);
            }, stoppingToken);
    }
}
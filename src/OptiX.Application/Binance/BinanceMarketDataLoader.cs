using Binance.Net.Clients;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OptiX.Application.Asset;
using OptiX.Application.MarketData;

namespace OptiX.Application.Binance;

public sealed class BinanceMarketDataLoader : BackgroundService
{
    private readonly BinanceSocketClient _binanceSocketClient;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<BinanceMarketDataLoader> _logger;

    public BinanceMarketDataLoader(ILogger<BinanceMarketDataLoader> logger, IServiceProvider serviceProvider)
    {
        _binanceSocketClient = new BinanceSocketClient();
        _logger = logger;
        _serviceProvider = serviceProvider;
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
                
                var assetId = symbolsAndAssetIds[update.Symbol!];
                var tick = new TickDto
                {
                    DateTime = update.Data.TradeTime,
                    Volume = update.Data.Quantity,
                    Price = update.Data.Price
                };

                await ticksService.SaveAsync(assetId, [tick]);
            }, stoppingToken);
    }
}
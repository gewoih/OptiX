using System.Threading.Channels;
using Binance.Net.Clients;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OptiX.Application.MarketData.Responses;
using OptiX.Application.MarketData.Services;
using OptiX.Application.SignalR;

namespace OptiX.Application.Binance;

public sealed class BinanceMarketDataLoader : BackgroundService
{
    private readonly BinanceSocketClient _binanceSocketClient;
    private readonly Channel<TickDto> _tickChannel;
    private readonly IHubContext<MarketDataHub> _hubContext;
    private readonly IServiceProvider _serviceProvider;

    public BinanceMarketDataLoader(IServiceProvider serviceProvider, IHubContext<MarketDataHub> hubContext)
    {
        _binanceSocketClient = new BinanceSocketClient();
        _serviceProvider = serviceProvider;
        _hubContext = hubContext;
        _tickChannel = Channel.CreateUnbounded<TickDto>();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var saveTicksTask = SaveTicksAsync(stoppingToken);
        var broadcastTicksTask = BroadcastTicksAsync(stoppingToken);
        var symbolsToSubscribe = new List<string> { "BTCUSDT" };

        var subscriptionResult = await _binanceSocketClient.SpotApi.ExchangeData.SubscribeToTradeUpdatesAsync(
            symbolsToSubscribe, async update =>
            {
                if (string.IsNullOrEmpty(update.Symbol))
                    return;

                var tick = new TickDto
                {
                    Id = update.Data.Id,
                    Symbol = update.Symbol,
                    Date = update.Data.TradeTime,
                    Volume = update.Data.Quantity,
                    Price = update.Data.Price,
                };

                _tickChannel.Writer.TryWrite(tick);
            }, stoppingToken);

        await Task.WhenAll(saveTicksTask, broadcastTicksTask);
    }

    private async Task SaveTicksAsync(CancellationToken stoppingToken)
    {
        var ticksBuffer = new List<TickDto>();
        var flushInterval = TimeSpan.FromSeconds(1);
        var nextFlush = DateTime.UtcNow.Add(flushInterval);

        while (!stoppingToken.IsCancellationRequested)
        {
            while (await _tickChannel.Reader.WaitToReadAsync(stoppingToken))
            {
                while (_tickChannel.Reader.TryRead(out var tick))
                {
                    ticksBuffer.Add(tick);

                    if (DateTime.UtcNow < nextFlush)
                        continue;

                    using var scope = _serviceProvider.CreateScope();
                    var ticksService = scope.ServiceProvider.GetRequiredService<IMarketDataService>();

                    try
                    {
                        await ticksService.SaveAsync(ticksBuffer);
                    }
                    catch (Exception ex)
                    {
                        // ignored
                    }

                    ticksBuffer.Clear();
                    nextFlush = DateTime.UtcNow.Add(flushInterval);
                }
            }
        }
    }

    private async Task BroadcastTicksAsync(CancellationToken stoppingToken)
    {
        while (await _tickChannel.Reader.WaitToReadAsync(stoppingToken))
        {
            while (_tickChannel.Reader.TryRead(out var tick))
            {
                try
                {
                    await _hubContext.Clients.Group(tick.Symbol)
                        .SendAsync("ReceiveMarketData", tick, cancellationToken: stoppingToken);
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., log them)
                }
            }
        }
    }
}
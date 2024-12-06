using MassTransit;
using OptiX.Application.Trades.Requests;
using OptiX.Application.Trades.Services;

namespace OptiX.Application.Trades.Messaging.Consumers;

public sealed class CloseTradeConsumer : IConsumer<CloseTradeRequest>
{
    private readonly ITradeService _tradeService;

    public CloseTradeConsumer(ITradeService tradeService)
    {
        _tradeService = tradeService;
    }

    public async Task Consume(ConsumeContext<CloseTradeRequest> context)
    {
        await _tradeService.CloseTradeAsync(context.Message);
    }
}
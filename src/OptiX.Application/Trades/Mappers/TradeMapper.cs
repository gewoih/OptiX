using OptiX.Application.Trades.Responses;
using OptiX.Domain.Entities.Trading;

namespace OptiX.Application.Trades.Mappers;

public static class TradeMapper
{
    public static TradeDto ToDto(this Trade trade)
    {
        return new TradeDto(trade.Id, trade.Status, trade.Direction, trade.OpenedAt, trade.ClosedAt, trade.OpenPrice,
            trade.ClosePrice, trade.Amount, trade.Commission, trade.GetProfit());
    }
}
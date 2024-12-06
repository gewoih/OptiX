using OptiX.Domain.Entities.Base;
using OptiX.Domain.ValueObjects;

namespace OptiX.Domain.Entities.Trading;

public sealed class Trade : Entity
{
    public Guid AccountId { get; set; }
    public string Symbol { get; set; }
    public TradeStatus Status { get; set; }
    public DateTime OpenedAt { get; set; }
    public DateTime ClosedAt { get; set; }
    public DateTime PlannedClosingDate { get; set; }
    public TradeDirection Direction { get; set; }
    public decimal Amount { get; set; }
    public decimal OpenPrice { get; set; }
    public decimal ClosePrice { get; set; }
    public decimal Commission { get; set; }
    public decimal? Profit { get; set; }
    public decimal OpenSum => OpenPrice * Amount;
    public decimal CloseSum => ClosePrice * Amount;

    public Trade()
    {
    }

    public Trade(Guid accountId, string Symbol, TradeDirection direction, TradeDurationMinutes durationMinutes, decimal amount,
        decimal openPrice)
    {
        AccountId = accountId;
        Symbol = Symbol;
        Direction = direction;
        OpenedAt = DateTime.UtcNow;
        PlannedClosingDate = OpenedAt.AddMinutes((int)durationMinutes);
        Status = TradeStatus.Opened;
        Amount = amount;
        OpenPrice = openPrice;
    }

    public void Close(decimal closePrice)
    {
        ClosedAt = DateTime.UtcNow;
        ClosePrice = closePrice;
        Status = TradeStatus.Closed;
        Commission = (OpenPrice + ClosePrice) * Amount * 0.001m;
        Profit = GetProfit();
    }

    private decimal? GetProfit()
    {
        if (Status != TradeStatus.Closed)
            return null;

        if (Direction == TradeDirection.Long)
            return (ClosePrice - OpenPrice) * Amount - Commission;

        return (OpenPrice - ClosePrice) * Amount - Commission;
    }
}
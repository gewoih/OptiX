using OptiX.Domain.ValueObjects;

namespace OptiX.Application.Trades.Requests;

public record OpenTradeRequest(Guid AccountId, Guid AssetId, TradeDirection Direction, TradeDurationMinutes DurationMinutes, decimal Amount);
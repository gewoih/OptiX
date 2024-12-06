using OptiX.Domain.ValueObjects;

namespace OptiX.Application.Trades.Requests;

public record OpenTradeRequest(Guid AccountId, string Symbol, TradeDirection Direction, TradeDurationMinutes DurationMinutes, decimal Amount);
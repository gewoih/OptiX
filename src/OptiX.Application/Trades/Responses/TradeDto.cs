using OptiX.Domain.ValueObjects;

namespace OptiX.Application.Trades.Responses;

public record TradeDto(
    Guid Id,
    TradeStatus Status,
    TradeDirection Direction,
    DateTime OpenedAt,
    DateTime ClosedAt,
    decimal OpenPrice,
    decimal ClosePrice,
    decimal Amount,
    decimal Commission,
    decimal? Profit);
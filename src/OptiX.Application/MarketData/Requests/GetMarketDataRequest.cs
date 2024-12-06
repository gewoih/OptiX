using OptiX.Domain.ValueObjects;

namespace OptiX.Application.MarketData.Requests;

public record GetMarketDataRequest(Guid AssetId, DateTime From, DateTime To, TimeFrame TimeFrame);
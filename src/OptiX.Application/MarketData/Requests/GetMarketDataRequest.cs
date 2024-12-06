using OptiX.Domain.ValueObjects;

namespace OptiX.Application.MarketData.Requests;

public record GetMarketDataRequest(string Symbol, DateTime From, DateTime To, TimeFrame TimeFrame);
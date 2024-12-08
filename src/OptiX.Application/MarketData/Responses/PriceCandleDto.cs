namespace OptiX.Application.MarketData.Responses;

public record PriceCandleDto(DateTime Date, decimal Open, decimal High, decimal Low, decimal Close, decimal Volume);
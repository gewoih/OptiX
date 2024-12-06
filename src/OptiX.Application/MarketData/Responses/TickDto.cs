namespace OptiX.Application.MarketData.Responses;

public sealed class TickDto
{
    public long Id { get; set; }
    public required string Symbol { get; set; }
    public long TimeStamp { get; set; }
    public decimal Price { get; set; }
    public decimal Volume { get; set; }
}
namespace OptiX.Application.MarketData;

public sealed class TickDto
{
    public DateTime DateTime { get; set; }
    public decimal Price { get; set; }
    public decimal Volume { get; set; }
}
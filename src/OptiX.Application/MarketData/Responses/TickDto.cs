namespace OptiX.Application.MarketData.Responses;

public sealed class TickDto
{
    public Guid AssetId { get; set; }
    public DateTime DateTime { get; set; }
    public decimal Price { get; set; }
    public decimal Volume { get; set; }
}
using OptiX.Domain.Entities.Base;

namespace OptiX.Domain.Entities.Asset;

public sealed class MarketData : Entity
{
    public Guid AssetId { get; set; }
    public decimal Open { get; set; }
    public decimal High { get; set; }
    public decimal Low { get; set; }
    public decimal Close { get; set; }
    public decimal Volume { get; set; }
}
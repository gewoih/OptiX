using OptiX.Domain.Entities.Base;
using OptiX.Domain.ValueObjects;

namespace OptiX.Domain.Entities.Asset;

public sealed class Asset : Entity
{
    public AssetType Type { get; set; }
    public ICollection<MarketData> MarketData { get; set; }
}
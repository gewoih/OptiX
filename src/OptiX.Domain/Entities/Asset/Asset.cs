using System.ComponentModel.DataAnnotations;
using OptiX.Domain.Entities.Base;
using OptiX.Domain.ValueObjects;

namespace OptiX.Domain.Entities.Asset;

public sealed class Asset : Entity
{
    [MaxLength(10)]
    public required string Symbol { get; set; }
    public AssetType Type { get; set; }
    public ICollection<Tick> MarketData { get; set; }
}
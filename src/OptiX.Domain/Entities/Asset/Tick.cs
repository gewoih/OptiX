using OptiX.Domain.Entities.Base;

namespace OptiX.Domain.Entities.Asset;

public sealed class Tick : Entity
{
    public Guid AssetId { get; set; }
    public DateTime Date { get; set; }
    public decimal Price { get; set; }
    public decimal Volume { get; set; }
}
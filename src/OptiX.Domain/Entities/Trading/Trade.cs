using OptiX.Domain.Entities.Base;

namespace OptiX.Domain.Entities.Trading;

public sealed class Trade : Entity
{
    public Guid AccountId { get; set; }
    public Guid AssetId { get; set; }
    public DateTime OpenedAt { get; set; }
    public DateTime ClosedAt { get; set; }
    public decimal Amount { get; set; }
    public decimal OpenPrice { get; set; }
    public decimal ClosePrice { get; set; }
    public decimal Commission { get; set; }
}
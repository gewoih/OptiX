using OptiX.Domain.Entities.Base;

namespace OptiX.Domain.Entities.User;

public sealed class Trade : Entity
{
    public Guid AccountId { get; set; }
    public Guid AssetId { get; set; }
    public DateTime OpenTime { get; set; }
    public DateTime CloseTime { get; set; }
    public decimal Amount { get; set; }
    public decimal OpenPrice { get; set; }
    public decimal ClosePrice { get; set; }
    public decimal Commission { get; set; }
}
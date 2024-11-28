using OptiX.Domain.Entities.Base;

namespace OptiX.Domain.Entities.Asset;

public sealed class Account : Entity
{
    public Guid UserId { get; set; }
    public bool IsDemo { get; set; }
    public ICollection<Trade>? Trades { get; set; }
}
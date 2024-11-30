using OptiX.Domain.Entities.Base;

namespace OptiX.Domain.Entities.Trading;

public sealed class Account : Entity
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public bool IsDemo { get; set; }
    public ICollection<Trade> Trades { get; set; } = [];
    public ICollection<Transaction> Transactions { get; set; } = [];
}
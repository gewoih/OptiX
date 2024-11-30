using OptiX.Domain.Entities.Base;
using OptiX.Domain.ValueObjects;

namespace OptiX.Domain.Entities.Trading;

public sealed class Transaction : Entity
{
    public Guid AccountId { get; set; }
    public required decimal Amount { get; set; }
    public required DateTime Date { get; set; }
    public required TransactionTrigger Trigger { get; set; }
}
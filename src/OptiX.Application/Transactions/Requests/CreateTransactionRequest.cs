using OptiX.Domain.ValueObjects;

namespace OptiX.Application.Transactions.Requests;

public record CreateTransactionRequest(Guid AccountId, TransactionTrigger Trigger, decimal Amount);
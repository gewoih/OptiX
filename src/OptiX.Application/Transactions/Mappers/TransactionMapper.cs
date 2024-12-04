using OptiX.Application.Transactions.Responses;
using OptiX.Domain.Entities.Trading;

namespace OptiX.Application.Transactions.Mappers;

public static class TransactionMapper
{
    public static TransactionDto ToDto(this Transaction transaction)
    {
        return new TransactionDto(transaction.Id, transaction.AccountId, transaction.Date, transaction.Amount);
    }
}
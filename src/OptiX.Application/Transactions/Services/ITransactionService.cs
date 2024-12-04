using OptiX.Application.Transactions.Requests;
using OptiX.Application.Transactions.Responses;

namespace OptiX.Application.Transactions.Services;

public interface ITransactionService
{
    Task<decimal> GetBalanceAsync(Guid accountId);
    Task<TransactionDto> CreateTransactionAsync(CreateTransactionRequest request);
}
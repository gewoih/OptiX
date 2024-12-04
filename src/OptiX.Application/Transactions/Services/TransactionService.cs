using Microsoft.EntityFrameworkCore;
using OptiX.Application.Transactions.Mappers;
using OptiX.Application.Transactions.Requests;
using OptiX.Application.Transactions.Responses;
using OptiX.Domain.Entities.Trading;
using Optix.Infrastructure.Database;

namespace OptiX.Application.Transactions.Services;

public sealed class TransactionService : ITransactionService
{
    private readonly AppDbContext _context;

    public TransactionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<decimal> GetBalanceAsync(Guid accountId)
    {
        return await _context.Transactions
            .Where(transaction => transaction.AccountId == accountId)
            .SumAsync(transaction => transaction.Amount);
    }

    public async Task<TransactionDto> CreateTransactionAsync(CreateTransactionRequest request)
    {
        var accountBalance = await GetBalanceAsync(request.AccountId);
        if (accountBalance + request.Amount < 0)
            throw new Exception("Insufficient funds");

        var transaction = new Transaction
        {
            AccountId = request.AccountId,
            Trigger = request.Trigger,
            Date = DateTime.UtcNow,
            Amount = request.Amount
        };
        
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();

        return transaction.ToDto();
    }
}
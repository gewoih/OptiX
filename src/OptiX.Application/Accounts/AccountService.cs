using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OptiX.Application.Accounts.Requests;
using OptiX.Application.Users;
using OptiX.Domain.Entities.Trading;
using OptiX.Domain.ValueObjects;
using Optix.Infrastructure.Database;

namespace OptiX.Application.Accounts;

public sealed class AccountService : IAccountService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly ICurrentUserService _currentUserService;

    public AccountService(AppDbContext context, IConfiguration configuration, ICurrentUserService currentUserService)
    {
        _context = context;
        _configuration = configuration;
        _currentUserService = currentUserService;
    }

    public async Task<AccountDto> CreateAsync(CreateAccountRequest request)
    {
        var isDemo = request.IsDemo;
        var currentUserId = _currentUserService.GetCurrentUserId();

        var initialTransactions =
            isDemo
                ? new List<Transaction>
                {
                    new()
                    {
                        Date = DateTime.UtcNow,
                        Amount = Convert.ToDecimal(_configuration["InitialDemoBalance"]),
                        Trigger = TransactionTrigger.DemoAccountInitialization
                    }
                }
                : [];

        var newAccount = new Account
        {
            UserId = currentUserId,
            IsDemo = isDemo,
            Name = request.Name,
            Transactions = initialTransactions,
        };

        await _context.Accounts.AddAsync(newAccount);
        await _context.SaveChangesAsync();

        return new AccountDto
        {
            Id = newAccount.Id,
            Name = newAccount.Name,
            IsDemo = newAccount.IsDemo,
            Balance = newAccount.Transactions.Sum(x => x.Amount),
        };
    }

    public async Task<List<AccountDto>> GetAllAsync(GetAllAccountsRequest request)
    {
        var currentUserId = _currentUserService.GetCurrentUserId();
        var getAccountsQuery = _context.Accounts.Where(account => account.UserId == currentUserId);

        if (request.DemoAccountsOnly)
            getAccountsQuery = getAccountsQuery.Where(account => account.IsDemo);

        var accounts = await getAccountsQuery
            .Include(account => account.Transactions)
            .Select(account => new AccountDto
            {
                Id = account.Id,
                IsDemo = account.IsDemo,
                Name = account.Name,
                Balance = account.Transactions.Sum(transaction => transaction.Amount),
            }).ToListAsync();

        return accounts;
    }
}
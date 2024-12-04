using OptiX.Application.Accounts.Requests;
using OptiX.Application.Accounts.Responses;

namespace OptiX.Application.Accounts.Services;

public interface IAccountService
{
    Task<AccountDto> CreateAsync(CreateAccountRequest request);
    Task<List<AccountDto>> GetAllAsync(GetAllAccountsRequest request);
    Task<AccountDto?> GetAsync(Guid id);
}
using OptiX.Application.Accounts.Requests;

namespace OptiX.Application.Accounts;

public interface IAccountService
{
    Task<AccountDto> CreateAsync(CreateAccountRequest request);
    Task<List<AccountDto>> GetAllAsync(GetAllAccountsRequest request);
}
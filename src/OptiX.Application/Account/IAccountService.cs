namespace OptiX.Application.Account;

public interface IAccountService
{
    Task<AccountDto> CreateAsync(CreateAccountRequest request);
    Task<List<AccountDto>> GetAllAsync(GetAllAccountsRequest request);
}
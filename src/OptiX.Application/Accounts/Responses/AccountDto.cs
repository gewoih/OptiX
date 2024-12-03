namespace OptiX.Application.Accounts.Responses;

public class AccountDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsDemo { get; set; }
    public decimal Balance { get; set; }
}
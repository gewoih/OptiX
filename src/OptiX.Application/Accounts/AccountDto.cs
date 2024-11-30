namespace OptiX.Application.Accounts;

public class AccountDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsDemo { get; set; }
    public decimal Balance { get; set; }
}
using Microsoft.AspNetCore.Identity;
using OptiX.Domain.Entities.Asset;
using OptiX.Domain.Entities.User;

namespace OptiX.Domain.Entities.Identity;

public sealed class User : IdentityUser<Guid>
{
    public required UserProfile Profile { get; set; }
    public required ICollection<Account> Accounts { get; set; }
}
using OptiX.Domain.Entities.Base;

namespace OptiX.Domain.Entities.User;

public sealed class UserProfile : Entity
{
    public DateOnly BirthDate { get; set; }
}
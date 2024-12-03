namespace OptiX.Application.Users;

public interface ICurrentUserService
{
    Guid GetCurrentUserId();
}
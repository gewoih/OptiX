namespace OptiX.Application.Users.Services;

public interface ICurrentUserService
{
    Guid GetCurrentUserId();
}
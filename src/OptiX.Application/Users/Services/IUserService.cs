using Microsoft.AspNetCore.Authentication;

namespace OptiX.Application.Users.Services;

public interface IUserService
{
    Task<string> LoginGoogleAsync();
    AuthenticationProperties GetGoogleAuthenticationProperties(string returnUrl);
}
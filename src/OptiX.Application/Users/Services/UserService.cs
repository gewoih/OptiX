using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OptiX.Domain.Entities.Identity;
using OptiX.Domain.Entities.User;

namespace OptiX.Application.Users.Services;

public class UserService : IUserService
{
    private readonly IConfiguration _configuration;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public UserService(SignInManager<User> signInManager, UserManager<User> userManager, IConfiguration configuration)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<string> LoginGoogleAsync()
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
            return string.Empty;

        var result =
            await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
        if (!result.Succeeded)
        {
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);

            var user = new User { UserName = email, Email = email, Accounts = [], Profile = new UserProfile() };
            var identityResult = await _userManager.CreateAsync(user);
            if (identityResult.Succeeded)
                await _userManager.AddLoginAsync(user, info);
        }

        var userFromDb = await _userManager.FindByEmailAsync(info.Principal.FindFirstValue(ClaimTypes.Email));
        var token = GenerateJwtToken(userFromDb);
        return token;
    }

    public AuthenticationProperties GetGoogleAuthenticationProperties(string redirectUrl)
    {
        return _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var jwtKey = _configuration["Jwt:Key"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //TODO: Разобраться как правильно
        var expires = DateTime.Now.AddDays(7);

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: expires,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
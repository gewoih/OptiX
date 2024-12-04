using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace OptiX.Application.Users.Services;

public sealed class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetCurrentUserId()
    {
        var nameIdentifierClaims = _httpContextAccessor.HttpContext?.User.Claims
            .Where(c => c.Type == ClaimTypes.NameIdentifier)
            .Select(c => c.Value)
            .ToList();

        if (nameIdentifierClaims is null || nameIdentifierClaims.Count == 0)
            return Guid.Empty;

        foreach (var claim in nameIdentifierClaims)
        {
            if (Guid.TryParse(claim, out var userId))
                return userId;
        }

        return Guid.Empty;
    }
}
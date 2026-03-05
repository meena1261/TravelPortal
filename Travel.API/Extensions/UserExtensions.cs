using System.Security.Claims;

namespace Travel.API.Extensions
{
    public static class UserExtensions
    {
        public static string? GetUserId(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public static string? GetEmail(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.Email)?.Value;

        public static string? GetRole(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.Role)?.Value;
    }
}

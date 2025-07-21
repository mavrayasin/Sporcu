using Sporcu.Entity;
using System.Security.Claims;

namespace Sporcu.Helpers
{
    public interface IJwtTokenHelper
    {
        string GenerateToken(UserSporcu user);
        ClaimsPrincipal ValidateToken(string token);
    }
}
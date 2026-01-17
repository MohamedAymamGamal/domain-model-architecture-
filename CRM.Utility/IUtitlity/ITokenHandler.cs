using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace CRM.Utility.IUtitlity
{
    public interface ITokenHandler
    {
        string GenerateJwtToken(List<Claim> claims);
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
        string GenerateRefreshToken();
        int GetRefreshTokenExpiryDays();
        int GetMaxRefreshTokenAttempts();
    }
}

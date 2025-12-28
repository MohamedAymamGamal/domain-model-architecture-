using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CRM.Utility.IUtitlity;
using System.Text;
using Microsoft.Extensions.Options;

namespace CRM.Utility
{
    public class TokenHandler(IOptions<TokenConfiguration> tokenConfig) : ITokenHandler
    {
        private readonly TokenConfiguration _tokenConfig = tokenConfig.Value;

        public string GenerateJwtToken(List<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfig.SecretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenValidity = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_tokenConfig.TokenExpiry));

            var token = new JwtSecurityToken(
                        issuer: _tokenConfig.Issuer,
                        audience: _tokenConfig.Audience,
                        claims: claims,
                        expires: tokenValidity,
                        signingCredentials: signingCredentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}

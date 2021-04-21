using Microsoft.IdentityModel.Tokens;
using Stocknize.Domain.Entities;
using Stocknize.Domain.Interfaces.Domain;
using Stocknize.Domain.Models.Jwt;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Stocknize.Domain.Services
{
    public class JwtGenerator : IJwtGenerator
    {
        private const string Issuer = "https://localhost:5001";
        private const string Secret = "VeryHyperMegaSuperSecretKey";
        private const double ExpirationInMinutes = 60;

        public Task<JwtModel> GenerateToken(User user)
        {
            var token = ConfigureToken(user);

            return Task.FromResult(new JwtModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresAt = DateTime.Now.AddMinutes(ExpirationInMinutes)
            });
        }

        private JwtSecurityToken ConfigureToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                    issuer: Issuer,
                    claims: GenerateUserListClaims(user),
                    expires: DateTime.Now.AddMinutes(ExpirationInMinutes),
                    signingCredentials: signingCredentials
                ); ;
        }

        private List<Claim> GenerateUserListClaims(User user)
        {
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Credentials.Login)
            };
        }
    }
}

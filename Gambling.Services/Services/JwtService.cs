using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Gambling.Common;
using Gambling.Entities.User;
using Gambling.Services.Contracts;
using Gambling.ViewModel.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Gambling.Services.Services
{
    public class JwtService : IJwtService
    {

        public readonly SiteSettings _siteSettings;
        public JwtService(IOptionsSnapshot<SiteSettings> siteSettings)
        {

            _siteSettings = siteSettings.Value;
        }

        public async Task<string> GenerateTokenAsync(User user)
        {
            var secretKey = Encoding.UTF8.GetBytes(_siteSettings.JwtSettings.SecretKey);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);


            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = _siteSettings.JwtSettings.Issuer,
                Audience = _siteSettings.JwtSettings.Audience,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(_siteSettings.JwtSettings.NotBeforeMinutes),
                Expires = DateTime.Now.AddMinutes(_siteSettings.JwtSettings.ExpirationMinutes),
                SigningCredentials = signingCredentials,
                Subject = new ClaimsIdentity(await GetClaimsAsync(user))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);

        }


        public async Task<IEnumerable<Claim>> GetClaimsAsync(User user)
        {
            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.MobilePhone,user.PhoneNumber),
                //new Claim(new ClaimsIdentityOptions().SecurityStampClaimType,user.SecurityStamp),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };


            return Claims;
        }
    }
}

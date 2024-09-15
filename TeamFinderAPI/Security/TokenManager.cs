using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;

namespace TeamFinderAPI.Security
{
    public static class TokenManager
    {
        private static string Secret = "asdasdasfbahfvasdasdasdasdasdadasdasdasdasdasdasd";
        
        public static string GenerateToken(string username, int expireTimeInMinutes = 30){
            var symmetricKey =  Encoding.UTF8.GetBytes(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            
            var tokenDescriptor = new SecurityTokenDescriptor(){
                Subject = new ClaimsIdentity(new []{
                    new Claim(ClaimTypes.Name,username)
                }),
                Claims = {
                    
                },
                Expires = now.AddMinutes(Convert.ToInt32(expireTimeInMinutes)),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }

        public static ClaimsPrincipal GetPrincipal(string token)
{
    try
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

        if (jwtToken == null)
            return null;

        var symmetricKey = Convert.FromBase64String(Secret);

        var validationParameters = new TokenValidationParameters()
        {
            RequireExpirationTime = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
        };

        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

        return principal;
    }
    catch (Exception)
    {
        //should write log
        return null;
    }
}
    }
}
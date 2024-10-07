using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace TeamFinderAPI.Service
{
    public class Utils
    {
        public async static Task<JwtSecurityToken> DecipherToken(HttpContext httpContext){
            HttpContext ctx = httpContext;
            var jwt = await ctx.GetTokenAsync("access_token");
            var decipher = new JwtSecurityTokenHandler();
            JwtSecurityToken obj = decipher.ReadJwtToken(jwt);

            return obj;
        }
    }
}
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TeamFinderAPI.Controllers.PostBody;
using TeamFinderAPI.Repository;
using TeamFinderAPI.Security;

namespace TeamFinderAPI.JwtAuthentication.Endpoints;

public static class TokenEndpoint
{
	//handles requests from /connect/token
    public static async Task<IResult> Connect(
    HttpContext ctx,
    JwtOptions jwtOptions, AuthBody body)
{
	

    
	//creates the access token (jwt token)
    var tokenExpiration = TimeSpan.FromSeconds(jwtOptions.ExpirationSeconds);
    var accessToken = TokenEndpoint.CreateAccessToken(
        jwtOptions,
        body.Name,
        TimeSpan.FromMinutes(60),
        new[] { "read_todo", "create_todo" });
	
    //returns a json response with the access token
    return Results.Ok(new
    {
        access_token = accessToken,
        expiration = (int)tokenExpiration.TotalSeconds,
        type = "bearer"
    });
}
    
    //
    static string CreateAccessToken(
    JwtOptions jwtOptions,
    string username,
    TimeSpan expiration,
    string[] permissions)
{
    var keyBytes = Encoding.UTF8.GetBytes(jwtOptions.SigningKey);
    var symmetricKey = new SymmetricSecurityKey(keyBytes);

    var signingCredentials = new SigningCredentials(
        symmetricKey,
        // 👇 one of the most popular. 
        SecurityAlgorithms.HmacSha256);

    var claims = new List<Claim>()
    {
        new Claim("sub", username),
        new Claim("name", username),
        new Claim("aud", jwtOptions.Audience)
    };
    
    var roleClaims = permissions.Select(x => new Claim("role", x));
    claims.AddRange(roleClaims);

    var token = new JwtSecurityToken(
        issuer: jwtOptions.Issuer,
        audience: jwtOptions.Audience,
        claims: claims,
        expires: DateTime.Now.Add(expiration),
        signingCredentials: signingCredentials);

    var rawToken = new JwtSecurityTokenHandler().WriteToken(token);
    return rawToken;
    }

    
}
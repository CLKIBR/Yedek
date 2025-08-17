using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NArchitecture.Core.Security.Entities;
using NArchitecture.Core.Security.JWT;

namespace Application.Services.AuthService;

public class CustomTokenHelper : ITokenHelper<Guid, int, Guid>
{
    private readonly TokenOptions _tokenOptions;
    public CustomTokenHelper(IConfiguration configuration)
    {
        _tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>()
            ?? throw new NullReferenceException("TokenOptions section not found in configuration");
    }

    public AccessToken CreateToken(User<Guid> user, IList<OperationClaim<int>> operationClaims)
    {
        var appUser = user as Domain.Entities.User;
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("position", ((int?)appUser?.Position)?.ToString() ?? "0"),
            new Claim("positionName", appUser?.Position.ToString() ?? "NotSpecified")
        };
        foreach (var operationClaim in operationClaims)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, operationClaim.Name));
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(_tokenOptions.AccessTokenExpiration);
        var jwtToken = new JwtSecurityToken(
            issuer: _tokenOptions.Issuer,
            audience: _tokenOptions.Audience,
            claims: authClaims,
            expires: expires,
            signingCredentials: creds
        );
        var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return new AccessToken { Token = token, ExpirationDate = expires };
    }

    public NArchitecture.Core.Security.Entities.RefreshToken<Guid, Guid> CreateRefreshToken(User<Guid> user, string ipAddress)
    {
        return new NArchitecture.Core.Security.Entities.RefreshToken<Guid, Guid>
        {
            UserId = user.Id,
            Token = Guid.NewGuid().ToString(),
            ExpirationDate = DateTime.UtcNow.AddDays(_tokenOptions.RefreshTokenTTL),
            CreatedByIp = ipAddress
        };
    }
}

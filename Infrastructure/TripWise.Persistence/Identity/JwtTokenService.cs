using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TripWise.Application.Abstractions.Identity;
using TripWise.Application.Common.Models;

namespace TripWise.Persistence.Identity;

public sealed class JwtTokenService(
    IOptions<JwtOptions> options) : IJwtTokenService
{
    private readonly JwtOptions _options = options.Value;

    public AccessTokenResponse GenerateAccessToken(Guid userId)
    {
        var now = DateTimeOffset.UtcNow;

        var expiresAt = now.AddMinutes(
            _options.AccessTokenExpirationMinutes);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(
                JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(
            Convert.FromBase64String(_options.Secret));

        var signingCredentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            notBefore: now.UtcDateTime,
            expires: expiresAt.UtcDateTime,
            signingCredentials: signingCredentials);

        var accessToken = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return new AccessTokenResponse(
            accessToken,
            expiresAt);
    }
}
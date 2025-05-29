using Application.Interfaces;
using Application.Responses;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Application.Handlers;

public class ValidateJwtTokenHandler : IValidateJwtTokenHandler
{
    private readonly string _jwtKey;

    public ValidateJwtTokenHandler(IConfiguration configuration)
    {
        _jwtKey = configuration["Jwt:Key"]!;
    }

    public ValidateTokenResponse Handle(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtKey);

        try
        {
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            };

            tokenHandler.ValidateToken(token, parameters, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            var claims = jwtToken.Claims.ToDictionary(c => c.Type, c => c.Value);

            return new ValidateTokenResponse
            {
                IsValid = true,
                Message = "Token válido.",
                SessionId = claims.TryGetValue("sessionTokenId", out var sessionId) ? Guid.Parse(sessionId) : null,
                ClientId = claims.TryGetValue("clientId", out var clientId) ? Guid.Parse(clientId) : null,
                Cpf = claims.TryGetValue("cpf", out var cpf) ? cpf : null,
                ExpireAt = claims.TryGetValue("expireAt", out var expireAt) ? DateTime.Parse(expireAt) : null
            };
        }
        catch (SecurityTokenExpiredException)
        {
            return new ValidateTokenResponse
            {
                IsValid = false,
                Message = "Token expirado."
            };
        }
        catch (Exception)
        {
            return new ValidateTokenResponse
            {
                IsValid = false,
                Message = "Token inválido."
            };
        }
    }
}
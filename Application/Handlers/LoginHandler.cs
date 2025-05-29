using Application.Interfaces;
using Application.Requests;
using Application.Responses;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Infra.Interfaces;

namespace Application.Handlers;

public class LoginHandler : ILoginHandler
{
    private readonly IClientRepository _clientRepository;
    private readonly IUserCredentialsRepository _credentialsRepository;
    private readonly ISessionTokenRepository _sessionTokenRepository;
    private readonly IConfiguration _configuration;

    public LoginHandler(
        IClientRepository clientRepository,
        IUserCredentialsRepository credentialsRepository,
        ISessionTokenRepository sessionTokenRepository,
        IConfiguration configuration)
        {
            _clientRepository = clientRepository;
            _credentialsRepository = credentialsRepository;
            _sessionTokenRepository = sessionTokenRepository;
            _configuration = configuration;
        }

    public LoginResponse Handle(LoginRequest request)
    {
        var client = _clientRepository.GetByEmail(request.Email);
        if (client == null)
            throw new ArgumentException("Cliente não encontrado.");
        
        var credentials = _credentialsRepository.GetByClientId(client.Id);
        if (credentials == null)
            throw new ArgumentException("Credenciais não encontradas.");
        
        var isPasswordValid = credentials.VerifyPassword(request.Password);
        if (!isPasswordValid)
            throw new ArgumentException("Senha inválida.");
        
        var (token, expireAt, refreshToken) = GenerateToken(client);
        
        var session = new SessionToken
        {
            Id = Guid.NewGuid(),
            ClientId = client.Id,
            Token = token,
            ExpiresAt = expireAt
        };

        _sessionTokenRepository.Save(session);
        
        return new LoginResponse
        {
            Token = token,
            RefreshToken = refreshToken,
            ExpireAt = expireAt
        };
    }

    private (string Token, DateTime ExpireAt, string RefreshToken) GenerateToken(Client client)
    {
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);
        var tokenHandler = new JwtSecurityTokenHandler();
        var expireAt = DateTime.UtcNow.AddHours(1);
        var sessionTokenId = Guid.NewGuid();

        var claims = new[]
        {
            new Claim("sessionTokenId", sessionTokenId.ToString()),
            new Claim("clientId", client.Id.ToString()),
            new Claim("cpf", client.Cpf),
            new Claim("expireAt", expireAt.ToString("o")) // ISO 8601 → padrão global de data
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expireAt,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return (tokenHandler.WriteToken(token), expireAt, string.Empty);
    }
}
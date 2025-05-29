using Application.Responses;

namespace Application.Interfaces;

public interface IValidateJwtTokenHandler
{
    ValidateTokenResponse Handle(string token);
}
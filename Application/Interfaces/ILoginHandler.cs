using Application.Requests;
using Application.Responses;

namespace Application.Interfaces;

public interface ILoginHandler
{
    LoginResponse Handle(LoginRequest request);
}
using Application.Requests;

namespace Application.Interfaces;

public interface ICreateUserCredentialsHandler
{
    void Handle(CreateCredentialsRequest request);
}
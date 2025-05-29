using Domain.Entities;

namespace Infra.Interfaces;

public interface IUserCredentialsRepository
{
    UserCredentials? GetByClientId(Guid clientId);
    void Save(UserCredentials credentials);
    void Update(UserCredentials credentials);
    void Delete(Guid clientId);
}
using Domain.Entities;

namespace Infra.Interfaces;

public interface ISessionTokenRepository
{
    SessionToken? GetByToken(string token);
    IEnumerable<SessionToken> GetByClientId(Guid clientId);
    void Save(SessionToken session);
    void Delete(Guid sessionId);
    void DeleteAllByClientId(Guid clientId);
}
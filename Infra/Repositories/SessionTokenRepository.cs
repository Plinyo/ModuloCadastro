using Domain.Entities;
using Infra.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infra.Repositories;

public class SessionTokenRepository : ISessionTokenRepository
{
    private readonly IMongoCollection<SessionToken> _collection;

    public SessionTokenRepository(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDb");
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("Cadastro");
        _collection = database.GetCollection<SessionToken>("SessionTokens");
    }

    public SessionToken? GetByToken(string token)
    {
        return _collection.Find(s => s.Token == token).FirstOrDefault();
    }

    public IEnumerable<SessionToken> GetByClientId(Guid clientId)
    {
        return _collection.Find(s => s.ClientId == clientId).ToList();
    }

    public void Save(SessionToken session)
    {
        _collection.InsertOne(session);
    }

    public void Delete(Guid sessionId)
    {
        _collection.DeleteOne(s => s.Id == sessionId);
    }

    public void DeleteAllByClientId(Guid clientId)
    {
        _collection.DeleteMany(s => s.ClientId == clientId);
    }
}
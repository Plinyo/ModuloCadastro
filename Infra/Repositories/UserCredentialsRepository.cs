using Domain.Entities;
using Infra.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infra.Repositories;

public class UserCredentialsRepository : IUserCredentialsRepository
{
    private readonly IMongoCollection<UserCredentials> _collection;

    public UserCredentialsRepository(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDb");

        var mongoClientSettings = MongoClientSettings.FromConnectionString(connectionString);

        var client = new MongoClient(mongoClientSettings);
        var database = client.GetDatabase("Cadastro");
        _collection = database.GetCollection<UserCredentials>("UserCredentials");
    }

    public UserCredentials? GetByClientId(Guid clientId)
    {
        return _collection.Find(c => c.ClientId == clientId).FirstOrDefault();
    }

    public void Save(UserCredentials credentials)
    {
        _collection.InsertOne(credentials);
    }

    public void Update(UserCredentials credentials)
    {
        _collection.ReplaceOne(c => c.ClientId == credentials.ClientId, credentials);
    }

    public void Delete(Guid clientId)
    {
        _collection.DeleteOne(c => c.ClientId == clientId);
    }
}
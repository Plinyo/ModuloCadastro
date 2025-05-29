using Domain.Entities;
using Domain.Enums;
using Infra.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infra.Repositories;

public class OtpRepository : IOtpRepository
{
    private readonly IMongoCollection<OtpCode> _collection;

    public OtpRepository(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDb");
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("Cadastro");
        _collection = database.GetCollection<OtpCode>("OtpCodes");
    }

    public void Save(OtpCode otp)
    {
        _collection.InsertOne(otp);
    }

    public void Update(OtpCode otp)
    {
        _collection.ReplaceOne(c => c.Id == otp.Id, otp);
    }

    public OtpCode? GetByClientIdAndChannel(Guid clientId, EOtpChannel channel)
    {
        return _collection
            .Find(c => c.ClientId == clientId && c.Channel == channel && c.IsValid)
            .FirstOrDefault();
    }

    public void Delete(Guid otpId)
    {
        _collection.DeleteOne(c => c.Id == otpId);
    }
}
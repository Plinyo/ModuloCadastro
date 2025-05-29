using Domain.Entities;
using Infra.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infra.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IMongoCollection<Client> _clients;

        public ClientRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDb");
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Cadastro");
            _clients = database.GetCollection<Client>("Clients");
        }

        public Client GetById(Guid id)
        {
            return _clients.Find(c => c.Id == id).FirstOrDefault();
        }
        
        public Client? GetByEmail(string email)
        {
            return _clients.Find(c => c.Emails.Contains(email)).FirstOrDefault();
        }
        
        public Client? GetByCpf(string cpf)
        {
            return _clients.Find(c => c.Cpf.Equals(cpf)).FirstOrDefault();
        }

        public void Save(Client client)
        {
            _clients.InsertOne(client);
        }
        public void Update(Client client)
        {
            var filter = Builders<Client>.Filter.Eq(c => c.Id, client.Id);

            var updates = new List<UpdateDefinition<Client>>();

            if (!string.IsNullOrEmpty(client.FullName))
                updates.Add(Builders<Client>.Update.Set(c => c.FullName, client.FullName));

            if (!string.IsNullOrEmpty(client.Cpf))
                updates.Add(Builders<Client>.Update.Set(c => c.Cpf, client.Cpf));

            if (client.DateOfBirth != null)
                updates.Add(Builders<Client>.Update.Set(c => c.DateOfBirth, client.DateOfBirth));

            if (client.Address != null)
                updates.Add(Builders<Client>.Update.Set(c => c.Address, client.Address));

            if (client.Phones != null && client.Phones.Any())
                updates.Add(Builders<Client>.Update.Set(c => c.Phones, client.Phones));

            if (client.Emails != null && client.Emails.Any())
                updates.Add(Builders<Client>.Update.Set(c => c.Emails, client.Emails));

            if (!updates.Any())
                return; // Nada para atualizar

            var updateDefinition = Builders<Client>.Update.Combine(updates);

            _clients.UpdateOne(filter, updateDefinition);
        }
    }
}
using Domain.Entities;

namespace Infra.Interfaces
{
    public interface IClientRepository
    {
        Client GetById(Guid id);
        void Save(Client client);
        void Update(Client client);
        Client? GetByEmail(string email);
        Client? GetByCpf(string cpf);
    }
}
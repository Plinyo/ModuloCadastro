using Application.Interfaces;
using Application.Responses;
using Infra.Interfaces;

namespace Application.Handlers
{
    public class GetClientByIdHandler : IGetClientByIdHandler
    {
        private readonly IClientRepository _clientRepository;

        public GetClientByIdHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public ClientResponse Handle(Guid clientId)
        {
            var client = _clientRepository.GetById(clientId);

            if (client == null)
                throw new ArgumentException("Cliente não encontrado.");

            return new ClientResponse
            {
                Id = client.Id,
                FullName = client.FullName,
                Cpf = client.Cpf,
                DateOfBirth = client.DateOfBirth,
                Address = new AddressResponse
                {
                    Street = client.Address.Street,
                    Number = client.Address.Number,
                    Complement = client.Address.Complement,
                    Neighborhood = client.Address.Neighborhood,
                    City = client.Address.City,
                    State = client.Address.State,
                    Country = client.Address.Country,
                    ZipCode = client.Address.ZipCode
                },
                Phones = client.Phones.Select(p => new PhoneResponse
                {
                    Number = p.Number,
                    Ddd = p.Ddd,
                    Ddi = p.Ddi
                }).ToList(),
                Emails = client.Emails
            };
        }
    }
}
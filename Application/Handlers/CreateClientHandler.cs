using Application.Interfaces;
using Application.Requests;
using Application.Responses;
using Domain.Entities;
using Infra.Interfaces;

namespace Application.Handlers
{
    public class CreateClientHandler : ICreateClientHandler
    {
        private readonly IClientRepository _repository;

        public CreateClientHandler(IClientRepository repository)
        {
            _repository = repository;
        }

        public CreateClientResponse Handle(ClientRequest request)
        {
            var client = Client.Builder()
                .SetFullName(request.FullName)
                .SetCpf(request.Cpf)
                .SetDateOfBirth(request.DateOfBirth)
                .SetAddress(
                    Address.Builder()
                        .SetStreet(request.Address.Street)
                        .SetNumber(request.Address.Number)
                        .SetComplement(request.Address.Complement)
                        .SetNeighborhood(request.Address.Neighborhood)
                        .SetCity(request.Address.City)
                        .SetState(request.Address.State)
                        .SetCountry(request.Address.Country)
                        .SetZipCode(request.Address.ZipCode)
                        .Build()
                )
                .SetPhones(
                    request.Phones.Select(phone => 
                        Phone.Builder()
                            .SetNumber(phone.Number)
                            .SetDdd(phone.Ddd)
                            .SetDdi(phone.Ddi)
                            .Build()
                    ).ToList()
                )
                .SetEmails(request.Emails)
                .Build();

            // Salvar no MongoDB via repositório
            _repository.Save(client);

            return new CreateClientResponse
            {
                Id = client.Id,
                Success = true
            };
        }
    }
}
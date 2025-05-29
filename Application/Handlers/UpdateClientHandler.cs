using Application.Interfaces;
using Application.Requests;
using Domain.Entities;
using Infra.Interfaces;

namespace Application.Handlers;

public class UpdateClientHandler : IUpdateClientHandler
{
    private readonly IClientRepository _clientRepository;

    public UpdateClientHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public void Handle(Guid clientId, UpdateClientRequest request)
    {
        var client = _clientRepository.GetById(clientId);

        if (client == null)
            throw new ArgumentException("Cliente não encontrado.");

        // Monta o Address utilizando o Builder
        var address = request.Address == null ? null :
            Address.Builder()
                .SetStreet(request.Address.Street)
                .SetNumber(request.Address.Number)
                .SetComplement(request.Address.Complement)
                .SetNeighborhood(request.Address.Neighborhood)
                .SetCity(request.Address.City)
                .SetState(request.Address.State)
                .SetCountry(request.Address.Country)
                .SetZipCode(request.Address.ZipCode)
                .Build();

        // Monta os Phones utilizando o Builder
        var phones = request.Phones?
            .Select(p => Phone.Builder()
                .SetNumber(p.Number)
                .SetDdd(p.Ddd)
                .SetDdi(p.Ddi)
                .Build())
            .ToList();

        // Atualiza o cliente
        client.Update(
            request.FullName,
            request.Cpf,
            request.DateOfBirth,
            address,
            phones,
            request.Emails
        );

        // Persiste a alteração
        _clientRepository.Update(client);
    }
}
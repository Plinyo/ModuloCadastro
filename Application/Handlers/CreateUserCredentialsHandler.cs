using Application.Interfaces;
using Application.Requests;
using Domain.Entities;
using Infra.Interfaces;

namespace Application.Handlers;

public class CreateUserCredentialsHandler : ICreateUserCredentialsHandler
{
    private readonly IClientRepository _clientRepository;
    private readonly IUserCredentialsRepository _credentialsRepository;

    public CreateUserCredentialsHandler(
        IClientRepository clientRepository,
        IUserCredentialsRepository credentialsRepository)
        {
            _clientRepository = clientRepository;
            _credentialsRepository = credentialsRepository;
        }

    public void Handle(CreateCredentialsRequest request)
    {
        var client = _clientRepository.GetByCpf(request.Cpf);
        if (client == null)
            throw new ArgumentException("Cliente não encontrado para o CPF informado.");

        var existing = _credentialsRepository.GetByClientId(client.Id);
        if (existing != null)
            throw new ArgumentException("Credenciais já cadastradas para este cliente.");

        var credentials = UserCredentials.Create(client.Id, request.Password);

        _credentialsRepository.Save(credentials);
    }
}
using Application.Requests;

namespace Application.Interfaces;

public interface IUpdateClientHandler
{
    void Handle(Guid clientId, UpdateClientRequest request);
}
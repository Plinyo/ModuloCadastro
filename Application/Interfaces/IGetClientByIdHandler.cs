using Application.Responses;

namespace Application.Interfaces;

public interface IGetClientByIdHandler
{
    ClientResponse Handle(Guid clientId);
}
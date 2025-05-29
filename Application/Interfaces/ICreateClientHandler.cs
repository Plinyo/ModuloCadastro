using Application.Requests;
using Application.Responses;

namespace Application.Interfaces;

public interface ICreateClientHandler
{
    CreateClientResponse Handle(ClientRequest request);
}
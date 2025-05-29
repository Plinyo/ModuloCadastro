using Application.Requests;
using Application.Responses;

namespace Application.Interfaces;

public interface IGenerateOtpHandler
{
    void Handle(GenerateOtpRequest request);
}
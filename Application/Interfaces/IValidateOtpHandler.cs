using Application.Requests;
using Application.Responses;

namespace Application.Interfaces;

public interface IValidateOtpHandler
{
    ValidateOtpResponse Handle(ValidateOtpRequest request);
}
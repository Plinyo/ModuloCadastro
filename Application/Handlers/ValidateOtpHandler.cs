using Application.Interfaces;
using Application.Requests;
using Application.Responses;
using Infra.Interfaces;

namespace Application.Handlers;

public class ValidateOtpHandler : IValidateOtpHandler
{
    private readonly IOtpRepository _otpRepository;

    public ValidateOtpHandler(IOtpRepository otpRepository)
    {
        _otpRepository = otpRepository;
    }

    public ValidateOtpResponse Handle(ValidateOtpRequest request)
    {
        var otp = _otpRepository.GetByClientIdAndChannel(request.ClientId, request.Channel);

        if (otp == null || !otp.IsValid)
            return new ValidateOtpResponse { IsValid = false, Message = "Código não encontrado ou inválido." };

        if (otp.ExpiresAt < DateTime.UtcNow)
            return new ValidateOtpResponse { IsValid = false, Message = "Código expirado." };

        if (otp.Code != request.Otp)
        {
            otp.Attempts += 1;
            _otpRepository.Update(otp);
            return new ValidateOtpResponse { IsValid = false, Message = "Código incorreto." };
        }

        otp.IsValid = false;
        _otpRepository.Update(otp);

        return new ValidateOtpResponse { IsValid = true, Message = "Código validado com sucesso." };
    }
}
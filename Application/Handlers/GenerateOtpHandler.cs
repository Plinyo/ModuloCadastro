using Application.Interfaces;
using Application.Requests;
using Domain.Entities;
using Infra.Interfaces;

namespace Application.Handlers;

public class GenerateOtpHandler : IGenerateOtpHandler
{
    private readonly IOtpRepository _otpRepository;

    public GenerateOtpHandler(IOtpRepository otpRepository)
    {
        _otpRepository = otpRepository;
    }

    public void Handle(GenerateOtpRequest request)
    {
        // Verifica se já existe um OTP válido
        var existingOtp = _otpRepository.GetByClientIdAndChannel(request.ClientId, request.Channel);

        if (existingOtp != null && existingOtp.ExpiresAt > DateTime.UtcNow)
        {
            // Já existe um OTP válido, não gera outro
            return;
        }

        // Gera um novo OTP
        var otp = new Random().Next(100000, 999999).ToString();

        var otpCode = new OtpCode
        {
            ClientId = request.ClientId,
            Code = otp,
            Channel = request.Channel,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddMinutes(5)
        };

        _otpRepository.Save(otpCode);
    }
}
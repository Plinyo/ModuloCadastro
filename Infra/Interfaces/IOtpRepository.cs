using Domain.Enums;

namespace Infra.Interfaces;

using Domain.Entities;

public interface IOtpRepository
{
    void Save(OtpCode otp);
    void Update(OtpCode otp);
    OtpCode? GetByClientIdAndChannel(Guid clientId, EOtpChannel channel);
    void Delete(Guid otpId);
}
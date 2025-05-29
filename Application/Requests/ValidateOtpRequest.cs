using Domain.Enums;

namespace Application.Requests;

public class ValidateOtpRequest
{
    public Guid ClientId { get; set; }
    public string Otp { get; set; } = string.Empty;
    public EOtpChannel Channel { get; set; }
}
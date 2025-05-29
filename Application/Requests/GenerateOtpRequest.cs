using Domain.Enums;

namespace Application.Requests;

public class GenerateOtpRequest
{
    public Guid ClientId { get; set; }
    public EOtpChannel Channel { get; set; } // Email ou SMS
}
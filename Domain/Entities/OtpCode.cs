using Domain.Enums;

namespace Domain.Entities;

public class OtpCode
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ClientId { get; set; }
    public string Code { get; set; } = string.Empty;
    public EOtpChannel Channel { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; }
    public int Attempts { get; set; } = 0;
    public bool IsValid { get; set; } = true;
}
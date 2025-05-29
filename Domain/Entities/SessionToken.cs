namespace Domain.Entities;

public class SessionToken : BaseEntity
{
    public Guid ClientId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}
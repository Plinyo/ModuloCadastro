namespace Domain.Entities;

public class UserCredentials : BaseEntity
{
    public Guid ClientId { get; private set; }
    public string PasswordHash { get; private set; } = string.Empty;
    public DateTime ExpireAt { get; private set; }

    private UserCredentials() { }

    public static UserCredentials Create(Guid clientId, string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Senha não pode ser vazia.");

        return new UserCredentials
        {
            ClientId = clientId,
            PasswordHash = HashPassword(password),
            ExpireAt = DateTime.UtcNow.AddYears(1) // Validade da senha opcional
        };
    }

    public bool VerifyPassword(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
    }

    public void RenewExpiration()
    {
        ExpireAt = DateTime.UtcNow.AddYears(1);
        UpdateTimestamp();
    }

    private static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}
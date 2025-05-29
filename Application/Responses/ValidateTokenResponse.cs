namespace Application.Responses;

public class ValidateTokenResponse
{
    public bool IsValid { get; set; }
    public string Message { get; set; } = string.Empty;
    public Guid? SessionId { get; set; }
    public Guid? ClientId { get; set; }
    public string? Cpf { get; set; }
    public DateTime? ExpireAt { get; set; }
}
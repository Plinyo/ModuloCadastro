namespace Application.Responses;

public class ValidateOtpResponse
{
    public bool IsValid { get; set; }
    public string Message { get; set; } = string.Empty;
}
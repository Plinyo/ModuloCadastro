namespace Application.Requests;

public class UpdateClientRequest
{
    public string? FullName { get; set; }
    public string? Cpf { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public AddressRequest? Address { get; set; }
    public List<PhoneRequest>? Phones { get; set; }
    public List<string>? Emails { get; set; }
}
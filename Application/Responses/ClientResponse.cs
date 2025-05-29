namespace Application.Responses
{
    public class ClientResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Cpf { get; set; }
        public DateTime DateOfBirth { get; set; }
        public AddressResponse Address { get; set; }
        public List<PhoneResponse> Phones { get; set; }
        public List<string> Emails { get; set; }
    }

    public class AddressResponse
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }

    public class PhoneResponse
    {
        public string Number { get; set; }
        public string Ddd { get; set; }
        public string Ddi { get; set; }
    }
}
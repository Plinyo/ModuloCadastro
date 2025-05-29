namespace Application.Requests
{
    public class ClientRequest
    {
        public string FullName { get; set; }
        public string Cpf { get; set; }
        public DateTime DateOfBirth { get; set; }
        public AddressRequest Address { get; set; }
        public List<PhoneRequest> Phones { get; set; }
        public List<string> Emails { get; set; }
    }

    public class AddressRequest
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

    public class PhoneRequest
    {
        public string Number { get; set; }
        public string Ddd { get; set; }
        public string Ddi { get; set; }
    }
}
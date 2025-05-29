namespace Domain.Entities
{
    public class Address
    {
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }

        public Address() { }

        // Builder
        public static AddressBuilder Builder()
        {
            return new AddressBuilder();
        }

        public class AddressBuilder
        {
            private Address _address;

            public AddressBuilder()
            {
                _address = new Address();
            }

            public AddressBuilder SetStreet(string street)
            {
                _address.Street = street;
                return this;
            }

            public AddressBuilder SetNumber(string number)
            {
                _address.Number = number;
                return this;
            }

            public AddressBuilder SetComplement(string complement)
            {
                _address.Complement = complement;
                return this;
            }

            public AddressBuilder SetNeighborhood(string neighborhood)
            {
                _address.Neighborhood = neighborhood;
                return this;
            }

            public AddressBuilder SetCity(string city)
            {
                _address.City = city;
                return this;
            }

            public AddressBuilder SetState(string state)
            {
                _address.State = state;
                return this;
            }

            public AddressBuilder SetCountry(string country)
            {
                _address.Country = country;
                return this;
            }

            public AddressBuilder SetZipCode(string zipCode)
            {
                _address.ZipCode = zipCode;
                return this;
            }

            public Address Build()
            {
                return _address;
            }
        }
    }
}
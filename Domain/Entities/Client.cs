namespace Domain.Entities;

    public class Client : BaseEntity
    {
        public string FullName { get; private set; }
        public string Cpf { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public Address Address { get; private set; }
        public List<Phone> Phones { get; private set; }
        public List<string> Emails { get; private set; }

        private Client() { }

        // Builder
        public static ClientBuilder Builder()
        {
            return new ClientBuilder();
        }
        public void Update(
            string fullName,
            string cpf,
            DateTime? dateOfBirth,
            Address address,
            List<Phone> phones,
            List<string> emails)
            {
                if (!string.IsNullOrWhiteSpace(fullName))
                    FullName = fullName;

                if (!string.IsNullOrWhiteSpace(cpf))
                    Cpf = cpf;

                if (dateOfBirth != default)
                    DateOfBirth = (DateTime)dateOfBirth;

                if (address != null)
                    Address = address;

                if (phones != null && phones.Any())
                    Phones = phones;

                if (emails != null && emails.Any())
                    Emails = emails;
            }

        public class ClientBuilder
        {
            private Client _client;

            public ClientBuilder()
            {
                _client = new Client();
            }

            public ClientBuilder SetFullName(string fullName)
            {
                _client.FullName = fullName;
                return this;
            }

            public ClientBuilder SetCpf(string cpf)
            {
                _client.Cpf = cpf;
                return this;
            }

            public ClientBuilder SetDateOfBirth(DateTime dateOfBirth)
            {
                _client.DateOfBirth = dateOfBirth;
                return this;
            }

            public ClientBuilder SetAddress(Address address)
            {
                _client.Address = address;
                return this;
            }

            public ClientBuilder SetPhones(List<Phone> phones)
            {
                _client.Phones = phones;
                return this;
            }

            public ClientBuilder SetEmails(List<string> emails)
            {
                _client.Emails = emails;
                return this;
            }

            public Client Build()
            {
                _client.Id = Guid.NewGuid(); // Gerar ID único
                return _client;
            }
        }
    }
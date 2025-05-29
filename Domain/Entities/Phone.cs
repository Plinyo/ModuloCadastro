namespace Domain.Entities
{
    public class Phone
    {
        public string Number { get; private set; }
        public string Ddd { get; private set; }
        public string Ddi { get; private set; }

        public Phone() { }

        // Builder
        public static PhoneBuilder Builder()
        {
            return new PhoneBuilder();
        }

        public class PhoneBuilder
        {
            private Phone _phone;

            public PhoneBuilder()
            {
                _phone = new Phone();
            }

            public PhoneBuilder SetNumber(string number)
            {
                _phone.Number = number;
                return this;
            }

            public PhoneBuilder SetDdd(string ddd)
            {
                _phone.Ddd = ddd;
                return this;
            }

            public PhoneBuilder SetDdi(string ddi)
            {
                _phone.Ddi = ddi;
                return this;
            }

            public Phone Build()
            {
                return _phone;
            }
        }
    }
}
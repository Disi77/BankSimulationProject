namespace Bank.Objects
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public override string ToString()
        {
            var street = string.IsNullOrEmpty(Street) ? string.Empty : Street;
            var streetNumber = string.IsNullOrEmpty(StreetNumber) ? string.Empty : StreetNumber;
            var postalCode = string.IsNullOrEmpty(PostalCode) ? string.Empty : PostalCode.Replace(" ", string.Empty);
            var city = string.IsNullOrEmpty(City) ? string.Empty : City;
            var country = string.IsNullOrEmpty(Country) ? string.Empty : Country;

            return $"{street} {streetNumber}, {postalCode} {city}, {country}";
        }
    }
}


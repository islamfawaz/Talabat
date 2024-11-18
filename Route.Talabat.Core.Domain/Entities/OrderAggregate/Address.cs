namespace Route.Talabat.Core.Domain.Entities.OrderAggregate
{
    public class Address
    {
        public Address()
        {
            
        }
        public Address(string firstName, string lName, string street, string city, string country)
        {
            FirstName = firstName;
            LName = lName;
            Street = street;
            City = city;
            Country = country;
        }

        public string FirstName { get; set; }

        public string LName { get; set; }

        public string Street { get; set; }

        public string City { get; set; }
        public string Country { get; set; }



    }
}

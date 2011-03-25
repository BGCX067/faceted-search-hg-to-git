namespace FacetedSearch.Tests
{
    internal class CountryCode
    {
        public CountryCode()
        {
            Code = 1;
        }
        public int Code { get; set; }
    }

    internal class Country
    {
        public Country()
        {
            CountryCode = new CountryCode();
        }

        public string Name { get; set; }
        public int Id { get; set; }
        public CountryCode CountryCode { get; set; }
    }

    internal class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public bool Male { get; set; }
        public Country Country { get; set; }
    }
}
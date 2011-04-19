namespace FacetedSearch.Web.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class PersonRepository
    {
        private IList<Person> _persons = new List<Person>
                                             {
                                                 new Person
                                                     {
                                                         Name = "Sasha",
                                                         Surname = "Smith",
                                                         Age = 12,
                                                         IsMarried = false,
                                                     },
                                                 new Person
                                                     {
                                                         Name = "George",
                                                         Surname = "Caroli",
                                                         Age = 45,
                                                         IsMarried = true,
                                                     },
                                                 new Person
                                                     {
                                                         Name = "Simon",
                                                         Surname = "Aloyts",
                                                         Age = 34,
                                                         IsMarried = false,
                                                     },
                                                 new Person
                                                     {
                                                         Name = "Leon",
                                                         Surname = "Ginsburg",
                                                         Age = 36,
                                                         IsMarried = false,
                                                     },
                                                 new Person
                                                     {
                                                         Name = "Timur",
                                                         Surname = "Bekmambetov",
                                                         Age = 35,
                                                         IsMarried = true,
                                                     },
                                             };

        public IQueryable<Person> GetAll()
        {
            return _persons.AsQueryable();
        }
    }
}
namespace Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using FacetedSearch;

    internal class Simulator
    {
        private readonly IList<User> _users = new List<User>();

        static Simulator()
        {
            FacatedSearch.Map<User>()
                .Property(_ => _.Male)
                .Range(_ => _.Age)
                .List(_ => _.Country.CountryCode.Code)
                .List(_ => _.Country.Id, "CountryId");
        }

        public Simulator()
        {
            AddUser("User1", 20, true, "Ukraine");
            AddUser("User2", 30, false, "Ukraine");
            AddUser("User3", 40, true, "Ukraine");
            AddUser("User4", 20, false, "Ukraine");
            AddUser("User5", 30, true, "Ukraine");
            AddUser("User6", 40, false, "Ukraine");
            AddUser("User7", 20, true);
            AddUser("User8", 30, false);
            AddUser("User9", 40, true);
            AddUser("User10", 20, false);
            AddUser("User11", 30, true);
            AddUser("User12", 40, false);
        }

        private void AddUser(string userName, int age, bool male, string coutry = "USA")
        {
            var user = new User();
            user.Age = age;
            user.Male = male;
            user.Name = userName;
            user.Country = coutry == "USA"
                               ? new Country {Id = 1, Name = "USA"}
                               : new Country {Id = 2, Name = "Ukraine"};

            _users.Add(user);
        }

        public static void Run(Dictionary<string, object> userChoice)
        {
            var simulator = new Simulator();

            Func<User, bool> findExpression = FacatedSearch.Expression<User>(userChoice);
            simulator.SimulateDbRequest(findExpression);
        }

        private void SimulateDbRequest(Func<User, bool> findExpression)
        {
            IEnumerable<User> sortedUsers = _users.Where(findExpression);
            PrintOutUsers(sortedUsers);
        }

        private void PrintOutUsers(IEnumerable<User> sortedUsers)
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("Total: {0}", sortedUsers.Count()));
            foreach (User sortedUser in sortedUsers)
            {
                sb.AppendLine("-----------------------------------------");
                sb.AppendFormat("User name: {0}\n", sortedUser.Name);
                sb.AppendFormat("Age: {0}\n", sortedUser.Age);
                sb.AppendFormat("Male: {0}\n", sortedUser.Male);
                sb.AppendFormat("Country: {0}\n", sortedUser.Country.Name);
            }

            Console.WriteLine(sb);
        }
    }
}
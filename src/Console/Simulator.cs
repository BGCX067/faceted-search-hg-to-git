namespace Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using FacetedSearch;
    using FacetedSearch.Mapping;

    internal class Simulator
    {
        private IList<User> _users = new List<User>();

        static Simulator()
        {
            FacatedSearch.Map<User>()
                .Set(_ => _.Male)
                .Set(_ => _.Age, PropertyMappingType.RangeValue);
        }

        public Simulator()
        {
            AddUser("User1", 20, true);
            AddUser("User2", 30, false);
            AddUser("User3", 40, true);
            AddUser("User4", 20, false);
            AddUser("User5", 30, true);
            AddUser("User6", 40, false);
            AddUser("User7", 20, true);
            AddUser("User8", 30, false);
            AddUser("User9", 40, true);
            AddUser("User10", 20, false);
            AddUser("User11", 30, true);
            AddUser("User12", 40, false);
        }

        private void AddUser(string userName, int age, bool male)
        {
            var user = new User();
            user.Age = age;
            user.Male = male;
            user.Name = userName;

            _users.Add(user);
        }

        public static void Run(Dictionary<string, object> userChoice)
        {
            var simulator = new Simulator();

            var findExpression = FacatedSearch.Expression<User>(userChoice);
            simulator.SimulateDbRequest(findExpression);
        }

        private void SimulateDbRequest(Func<User, bool> findExpression)
        {
            var sortedUsers = _users.Where(findExpression);
            PrintOutUsers(sortedUsers);
        }

        private void PrintOutUsers(IEnumerable<User> sortedUsers)
        {
            var sb = new StringBuilder();
            foreach (var sortedUser in sortedUsers)
            {
                sb.AppendLine("-----------------------------------------");
                sb.AppendFormat("User name: {0}\n", sortedUser.Name);
                sb.AppendFormat("Age: {0}\n", sortedUser.Age);
                sb.AppendFormat("Male: {0}\n", sortedUser.Male);
            }

            Console.WriteLine(sb);
        }
    }
}
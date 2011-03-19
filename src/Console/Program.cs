namespace Console
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main()
        {
            SelectionFor(true, 20, 25);
            SelectionFor(true, 20, 45);
            SelectionFor(false, 20, 20);

            Console.ReadKey();
        }

        private static void SelectionFor(bool male, int ageFrom, int ageTo)
        {
            var userChoice = new Dictionary<string, object>();
            userChoice.Add("Male", male);
            userChoice.Add("Age", new Tuple<int, int>(ageFrom, ageTo));
            userChoice.Add("CountryId", new List<object> { 1, 2 });
            userChoice.Add("Country.CountryCode.Code", new List<object> { 1 });

            Simulator.Run(userChoice);
        }
    }
}

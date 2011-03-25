namespace FacetedSearch.Tests.Mapping
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    public class FacatedSearchMapperTests
    {
        [SetUp]
        public void BeforeEachTest()
        {
            FacatedSearch.Clear();
        }

        [Test]
        public void CanMapObjectPropertiesTest()
        {
            FacatedSearch.Map<User>()
                .Property(u => u.Male)
                .Range(u => u.Age)
                .List(u => u.Country.Id);
        }

        [Test]
        public void CanGetExpressionForMappedPropertiesTest()
        {
            FacatedSearch.Map<User>()
                .Property(u => u.Male)
                .Range(u => u.Age)
                .List(u => u.Country.Id);

            var userChoice = new Dictionary<string, object>();
            userChoice.Add("Male", true);
            userChoice.Add("Age", new Tuple<int, int>(18, 45));
            userChoice.Add("Country.Id", new List<object>
            {
                1,
                2
            });

            FacatedSearch.Expression<User>(userChoice);
        }

        [Test]
        [ExpectedException(
            ExpectedException = typeof(ArgumentException),
            ExpectedMessage = "Expression must contains expression for object property access")]
        public void CanMapOnlyObjectPropertyExpressionTest()
        {
            FacatedSearch.Map<User>().Property(u => false);
        }

        [Test]
        public void ShouldBuildExpressionOnlyForUserChoicedPropertiesTest()
        {
            FacatedSearch.Map<User>()
                .Property(u => u.Male)
                .Range(u => u.Age)
                .List(u => u.Country.Id);

            var userChoice = new Dictionary<string, object>();
            userChoice.Add("Male", true);

            FacatedSearch.Expression<User>(userChoice);
        }
    }
}
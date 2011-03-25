namespace FacetedSearch.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class FacatedSearchTests
    {
        [SetUp]
        public void BeforeEachTest()
        {
            FacatedSearch.Clear();
        }

        [Test]
        public void CanMapFacatedSearchTest()
        {
            var facatedSearchMapper = FacatedSearch.Map<User>();
            Assert.NotNull(facatedSearchMapper);
        }

        [Test]
        [ExpectedException(
            ExpectedException = typeof(ArgumentException), 
            ExpectedMessage = "Type 'FacetedSearch.Tests.User' already mapped")]
            
        public void CannotDuplilcateMappingTypesTest()
        {
            FacatedSearch.Map<User>();
            FacatedSearch.Map<User>();
        }

        [Test]
        [ExpectedException(
            ExpectedException = typeof(ArgumentException),
            ExpectedMessage = "Type 'FacetedSearch.Tests.User' is not mapped")]
        public void CannotGetExpressionForNotMappedTypeTest()
        {
            FacatedSearch.Expression<User>(null);
        }

        [Test]
        public void CanSuccessfullyGetExpressionForMappedTypeTest()
        {
            FacatedSearch.Map<User>();
            FacatedSearch.Expression<User>(null);
        }
    }
}
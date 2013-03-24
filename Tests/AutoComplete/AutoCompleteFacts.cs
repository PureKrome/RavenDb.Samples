using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using WorldDomination.Raven.Tests.Helpers;
using Xunit;

namespace Tests.AutoComplete
{
    // ReSharper disable InconsistentNaming

    public class Person_AutoCompleteFacts : RavenDbTestBase
    {
        private static IEnumerable<Person> People
        {
            get
            {
                return new List<Person>
                           {
                               new Person {FirstName = "John", Surname = "Adam", Age = 30},
                               new Person {FirstName = "John", Surname = "Adams", Age = 30},
                               new Person {FirstName = "John", Surname = "Adamanski", Age = 30},
                               new Person {FirstName = "Jane", Surname = "Adam", Age = 30},
                               new Person {FirstName = "Jane", Surname = "Adams", Age = 30},
                               new Person {FirstName = "John", Surname = "Baker", Age = 30},
                               new Person {FirstName = "Leah", Surname = "Culver", Age = 30}
                           };
            }
        }

        [Fact]
        public void GivenTheQueryJohnAdams_Search_Returns1Person()
        {
            // Arrange.
            DataToBeSeeded = new List<IEnumerable>{ People };
            IndexesToExecute = new List<Type>{ typeof(Person_AutoComplete)};
            const string query = "John Adam";

            // Act.
            // Only search for John -AND- Adam.
            var results = DocumentSession.Query<Person_AutoComplete.Result, Person_AutoComplete>()
                                         .Search(x => x.Query, query,
                                                 escapeQueryOptions: EscapeQueryOptions.AllowPostfixWildcard)
                                         .As<dynamic>()
                                         .ToList();

            // Assert.
            Assert.NotNull(results);
            Assert.NotEmpty(results);
            Assert.Equal(1, results.Count);
        }

        [Fact]
        public void GivenTheQueryJohnAdamWildcard_Search_Returns3People()
        {
            // Arrange.
            DataToBeSeeded = new List<IEnumerable> { People };
            IndexesToExecute = new List<Type> { typeof(Person_AutoComplete) };
            const string query = "John Adam*";

            // Act.
            // Only search for John -AND- Adam*.
            var results = DocumentSession.Query<Person_AutoComplete.Result, Person_AutoComplete>()
                                         .Search(x => x.Query, query,
                                                 escapeQueryOptions: EscapeQueryOptions.AllowPostfixWildcard)
                                         .As<dynamic>()
                                         .ToList();

            // Assert.
            Assert.NotNull(results);
            Assert.NotEmpty(results);
            Assert.Equal(3, results.Count);
        }
    }

    // ReSharper restore InconsistentNaming
}
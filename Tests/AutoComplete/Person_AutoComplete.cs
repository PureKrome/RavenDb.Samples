using System.Linq;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Tests.AutoComplete
{
    // ReSharper disable InconsistentNaming

    public class Person_AutoComplete : AbstractMultiMapIndexCreationTask<Person_AutoComplete.Result>
    {
        public Person_AutoComplete()
        {
            AddMap<Person>(people => from person in people
                                         select new Result()
                                                    {
                                                        Query = new object[]
                                                                    {
                                                                        person.FirstName,
                                                                        person.Surname
                                                                    }
                                                    });

            Index(x => x.Query, FieldIndexing.Analyzed);
        }

        public class Result
        {
            public object[] Query { get; set; }
        }
    }


    // ReSharper restore InconsistentNaming
}
namespace FacetedSearch.QueryBuilder
{
    using System;
    using System.Collections.Generic;
    using Lokad;
    using Params;
    using SD;

    public class ValueExtractor : IValueExtractor
    {
        public IDictionary<string, object> GetValueDictionary(SearchOptionsSD searchOptionsSD,
                                                              Func<ISearchOptionsParam, Pair<bool, object>>
                                                                  visitorMapper = null)
        {
            var visitor = new DictionaryQueryVisitor(visitorMapper);
            searchOptionsSD.Items.ForEach(_ => visitor.BuildPart(_));
            return visitor.GetResult();
        }
    }
}
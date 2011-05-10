namespace FacetedSearch
{
    using System;
    using System.Collections.Generic;
    using Mapping;
    using QueryBuilder;
    using SD;

    public class FacatedSearch
    {
        private static readonly Dictionary<Type, FacatedSearchMapper> ActiveMappings =
            new Dictionary<Type, FacatedSearchMapper>();

        public static void Clear()
        {
            ActiveMappings.Clear();
        }

        public static FacatedSearchMapper<T> Map<T>() where T : new()
        {
            if (ActiveMappings.ContainsKey(typeof (T)))
                throw new ArgumentException(String.Format("Type '{0}' already mapped", typeof (T)));

            var mapper = new FacatedSearchMapper<T>();
            ActiveMappings.Add(typeof (T), mapper);

            return mapper;
        }

        public static Func<T, bool> Expression<T>(Dictionary<string, object> userChoice)
        {
            if (!ActiveMappings.ContainsKey(typeof (T)))
                throw new ArgumentException(String.Format("Type '{0}' is not mapped", typeof (T)));

            var facatedMapper = (FacatedSearchMapper<T>) ActiveMappings[typeof (T)];
            return facatedMapper.Execute(userChoice);
        }
    }

    public class FacatedSearch<TModel> : FacatedSearch
    {
        private readonly Func<SearchOptions<TModel>> _searchOptionsInitializer;

        public FacatedSearch(Func<SearchOptions<TModel>> searchOptionsInitializer)
        {
            _searchOptionsInitializer = searchOptionsInitializer;
        }

        public Func<TModel, bool> GetQueryExpression(SearchOptionsSD searchOptionsSD)
        {
            var values = new ValueExtractor().GetValueDictionary(searchOptionsSD);
            var searchOptions = _searchOptionsInitializer();
            return searchOptions.QueryMapper.Execute(values);
        }
    }
}
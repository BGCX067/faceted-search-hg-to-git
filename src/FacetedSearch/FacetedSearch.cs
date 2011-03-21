namespace FacetedSearch
{
    using System;
    using System.Collections.Generic;
    using Mapping;

    public class FacatedSearch
    {
        private static readonly Dictionary<Type, FacatedSearchMapper> ActiveMappings = new Dictionary<Type, FacatedSearchMapper>();

        public static FacatedSearchMapper<T> Map<T>() where T : new()
        {
            var mapper = new FacatedSearchMapper<T>();
            ActiveMappings.Add(typeof(T), mapper);

            return mapper;
        }

        public static Func<T, bool> Expression<T>(Dictionary<string, object> userChoice)
        {
            var facatedMapper = (FacatedSearchMapper<T>)ActiveMappings[typeof(T)];
            return facatedMapper.Execute(userChoice);
        }
    }
}
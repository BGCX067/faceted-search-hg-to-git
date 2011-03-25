namespace FacetedSearch
{
    using System;
    using System.Collections.Generic;
    using Mapping;

    public class FacatedSearch
    {
        private static readonly Dictionary<Type, FacatedSearchMapper> ActiveMappings = new Dictionary<Type, FacatedSearchMapper>();

        public static void Clear()
        {
            ActiveMappings.Clear();
        }

        public static FacatedSearchMapper<T> Map<T>() where T : new()
        {
            if (ActiveMappings.ContainsKey(typeof(T)))
                throw new ArgumentException(string.Format("Type '{0}' already mapped", typeof(T)));

            var mapper = new FacatedSearchMapper<T>();
            ActiveMappings.Add(typeof(T), mapper);

            return mapper;
        }

        public static Func<T, bool> Expression<T>(Dictionary<string, object> userChoice)
        {
            if(!ActiveMappings.ContainsKey(typeof(T)))
                throw new ArgumentException(string.Format("Type '{0}' is not mapped", typeof(T)));

            var facatedMapper = (FacatedSearchMapper<T>)ActiveMappings[typeof(T)];
            return facatedMapper.Execute(userChoice);
        }
    }
}
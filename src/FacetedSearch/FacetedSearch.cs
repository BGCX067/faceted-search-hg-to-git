namespace FacetedSearch
{
    using System;
    using System.Collections.Generic;
    using Mapping;

    public class FacatedSearch
    {
        private static Dictionary<Type, FacatedSearchMapper> _activeMappings = new Dictionary<Type, FacatedSearchMapper>();

        public static FacatedSearchMapper<T> Map<T>() where T : new()
        {
            var mapper = new FacatedSearchMapper<T>();
            _activeMappings.Add(typeof(T), mapper);

            return mapper;
        }

        public static Func<T, bool> Expression<T>(Dictionary<string, object> userChoice)
        {
            var facatedMapper = (FacatedSearchMapper<T>)_activeMappings[typeof(T)];
            return facatedMapper.Execute(userChoice);
        }
    }
}
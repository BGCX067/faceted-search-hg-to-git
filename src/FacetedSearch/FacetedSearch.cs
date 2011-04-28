namespace FacetedSearch
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Json;
    using Mapping;
    using SD;

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
                throw new ArgumentException(String.Format("Type '{0}' already mapped", typeof(T)));

            var mapper = new FacatedSearchMapper<T>();
            ActiveMappings.Add(typeof(T), mapper);

            return mapper;
        }

        public static Func<T, bool> Expression<T>(Dictionary<string, object> userChoice)
        {
            if(!ActiveMappings.ContainsKey(typeof(T)))
                throw new ArgumentException(String.Format("Type '{0}' is not mapped", typeof(T)));

            var facatedMapper = (FacatedSearchMapper<T>)ActiveMappings[typeof(T)];
            return facatedMapper.Execute(userChoice);
        }

        public static object DeserializeJsonStream(Stream stream, IJsonSerializer jsonSerializer, Type deserializationType = null)
        {
            var reader = new StreamReader(stream);
            string json = reader.ReadToEnd();
            if (String.IsNullOrEmpty(json))
            {
                // no JSON data
                return null;
            }

            object obj;
            try
            {
                obj = jsonSerializer.Deserialize(json, deserializationType ?? typeof (SearchOptionsSD));
            }
            catch (Exception)
            {
                return null;
            }
            return obj;
        }
        
        public static object DeserializeJsonStream<T>(Stream stream, IJsonSerializer jsonSerializer)
        {
            return DeserializeJsonStream(stream, jsonSerializer, typeof (T));
        }
    }
}
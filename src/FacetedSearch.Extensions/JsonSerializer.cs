namespace FacetedSearch.Extensions
{
    using System;
    using Json;
    using Newtonsoft.Json;

    public class JsonSerializer : IJsonSerializer
    {
        #region IJsonSerializer Members

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public object Deserialize(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type);
        }

        #endregion
    }
}
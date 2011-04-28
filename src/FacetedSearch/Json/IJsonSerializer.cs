namespace FacetedSearch.Json
{
    using System;

    public interface IJsonSerializer
    {
        string Serialize(object obj);
        T Deserialize<T>(string json);
        object Deserialize(string json, Type type);
    }
}
using System;
using System.Collections.Generic;
using FacetedSearch.Params;
using Newtonsoft.Json;

namespace FacetedSearch
{
    public class JsonSerializer : IJsonSerializer
    {
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
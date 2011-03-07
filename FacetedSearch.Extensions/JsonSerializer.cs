using Newtonsoft.Json;

namespace FacetedSearch.Extensions
{
    public class JsonSerializer : IJsonSerializer
    {
        #region IJsonSerializer Members

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        #endregion
    }
}
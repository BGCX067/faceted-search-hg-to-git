using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace FacetedSearch
{
    public class DefaultJsonSerializer : IJsonSerializer
    {
        #region IJsonSerializer Members

        public string Serialize(object obj)
        {
            var jsonSerializer = new DataContractJsonSerializer(obj.GetType());

            string json;
            using (var memoryStream = new MemoryStream())
            {
                jsonSerializer.WriteObject(memoryStream, obj);

                json = Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            return json;
        }

        #endregion
    }
}
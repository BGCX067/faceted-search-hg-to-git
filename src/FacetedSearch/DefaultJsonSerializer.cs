using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using FacetedSearch.Extensions;
using FacetedSearch.SD;

namespace FacetedSearch
{
    public class DefaultJsonSerializer : IJsonSerializer
    {
        private static readonly IEnumerable<Type> KnowTypes;

        static DefaultJsonSerializer()
        {
            KnowTypes =
                AppDomain.CurrentDomain.GetAssemblies()
                    .Where(_ => _.IsDefined(typeof (SerializationTypesAttribute), false))
                    .SelectMany(
                        _ => _.GetExportedTypes()
                                 .Where(t => t.IsImplementationOf<ISD>())
                    );
        }

        #region IJsonSerializer Members

        public string Serialize(object obj)
        {
            var jsonSerializer = new DataContractJsonSerializer(obj.GetType(), KnowTypes);

            string json;
            using (var memoryStream = new MemoryStream())
            {
                jsonSerializer.WriteObject(memoryStream, obj);

                json = Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            return json;
        }
        
        public T Deserialize<T>(string json)
        {
            return (T)Deserialize(json, typeof(T));
        }

        public object Deserialize(string json, Type type)
        {
            var jsonSerializer = new DataContractJsonSerializer(type, KnowTypes);

            using (var memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                return jsonSerializer.ReadObject(memoryStream);
            }
        }

        #endregion
    }
}
namespace FacetedSearch.Web
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Web.Mvc;
    using System.Xml;

    public class DataContractValueProviderFactory : ValueProviderFactory
    {
        public DataContractValueProviderFactory()
        {
        }

        public DataContractValueProviderFactory(Type type, IEnumerable<Type> knownTypes) : this()
        {
            Type = type;
            KnownTypes = knownTypes;
        }

        protected static IEnumerable<Type> KnownTypes { get; set; }

        protected static Type Type { get; set; }

        private static void AddToBackingStore(Dictionary<string, object> backingStore, string prefix, object value)
        {
            var d = value as IDictionary<string, object>;
            if (d != null)
            {
                foreach (var entry in d)
                {
                    AddToBackingStore(backingStore, MakePropertyKey(prefix, entry.Key), entry.Value);
                }
                return;
            }

            var l = value as IList;
            if (l != null)
            {
                for (int i = 0; i < l.Count; i++)
                {
                    AddToBackingStore(backingStore, MakeArrayKey(prefix, i), l[i]);
                }
                return;
            }

            // primitive
            backingStore[prefix] = value;
        }

        private static object GetDeserializedObject(ControllerContext controllerContext)
        {
            if (
                !controllerContext.HttpContext.Request.ContentType.StartsWith("application/json",
                                                                              StringComparison.OrdinalIgnoreCase))
            {
                // not JSON request
                return null;
            }

            var reader = new StreamReader(controllerContext.HttpContext.Request.InputStream);
            string bodyText = reader.ReadToEnd();
            if (String.IsNullOrEmpty(bodyText))
            {
                // no JSON data
                return null;
            }

            object jsonData;
            using (var xmlReader = new XmlTextReader(new StringReader(bodyText)))
            {
                try
                {
                    var serializer = new DataContractJsonSerializer(Type, KnownTypes);
                    jsonData = serializer.ReadObject(xmlReader);
                }
                catch (Exception)
                {
                    return null;
                }
                
            }
            return jsonData;
        }

        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }

            object jsonData = GetDeserializedObject(controllerContext);
            if (jsonData == null)
            {
                return null;
            }

            var backingStore = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            AddToBackingStore(backingStore, String.Empty, jsonData);
            return new DictionaryValueProvider<object>(backingStore, CultureInfo.CurrentCulture);
        }

        private static string MakeArrayKey(string prefix, int index)
        {
            return prefix + "[" + index.ToString(CultureInfo.InvariantCulture) + "]";
        }

        private static string MakePropertyKey(string prefix, string propertyName)
        {
            return (String.IsNullOrEmpty(prefix)) ? propertyName : prefix + "." + propertyName;
        }
    }
}
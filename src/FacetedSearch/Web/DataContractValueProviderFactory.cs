namespace FacetedSearch.Web
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Web.Mvc;
    using Json;
    using SD;

    public class DataContractValueProviderFactory : ValueProviderFactory
    {
        private readonly IJsonSerializer _jsonSerializer;

        public DataContractValueProviderFactory()
        {
            JsonParamName = "json";
        }

        public DataContractValueProviderFactory(IJsonSerializer jsonSerializer) : this()
        {
            _jsonSerializer = jsonSerializer;
        }

        public IJsonSerializer JsonSerializer
        {
            get { return _jsonSerializer ?? new DefaultJsonSerializer(); }
        }

        public string JsonParamName { get; set; }

        public Type DeserializationType { get; set; }

        private void AddToBackingStore(IDictionary<string, object> backingStore, string prefix, object value)
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

            var list = value as IList;
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    AddToBackingStore(backingStore, MakeArrayKey(prefix, i), list[i]);
                }
                return;
            }

            // primitive
            backingStore[JsonParamName] = value;
        }

        private object GetDeserializedObject(ControllerContext controllerContext)
        {
            if (!controllerContext.HttpContext.Request.ContentType.StartsWith("application/json",
                                                                              StringComparison.OrdinalIgnoreCase))
            {
                // not JSON request
                return null;
            }

            return FacatedSearch.DeserializeJsonStream(controllerContext.HttpContext.Request.InputStream, JsonSerializer, DeserializationType);
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
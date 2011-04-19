namespace FacetedSearch.Web
{
    using System.Linq;
    using System.Reflection;
    using SD;

    public class FacetedOptions
    {
        private static readonly PropertyInfo[] PropertyInfos;

        static FacetedOptions()
        {
            PropertyInfos = typeof (FacetedOptions).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        public FacetedOptions()
        {
            url = string.Empty;
        }

        public FacetedOptions(object values)
        {
            if (values != null)
            {
                values.GetType()
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Join(PropertyInfos, _ => _.Name,
                          _ => _.Name,
                          (p1, p2) =>
                          new
                              {
                                  SourceProperty = p1,
                                  DestProperty = p2
                              }).
                    ForEach(_ => _.DestProperty
                                     .SetValue(this, _.SourceProperty.GetValue(values, null),
                                               null));
            }
        }

// ReSharper disable InconsistentNaming
        public SearchOptionsSD searchOptions { get; set; }


        public bool createUI { get; set; }

        public bool deferredUpdate { get; set; }

        public string url { get; set; }

        public string contentType { get; set; }
// ReSharper restore InconsistentNaming
    }
}
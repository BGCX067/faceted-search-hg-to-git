using System.Collections.Generic;

namespace FacetedSearch.SD
{
    public class SearchOptionsSD : ISD
    {
        public IList<ISD> Items { get; set; }

        public string HtmlData { get; set; }

        public string HtmlContainerName { get; set; }
    }
}
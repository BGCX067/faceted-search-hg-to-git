namespace FacetedSearch.SD
{
    using System.Collections.Generic;

    public class SearchOptionsSD : ISD
    {
        //List type was used to be compatible with deserialization process of DataContractSerializer
        public List<ISD> Items { get; set; }

        public string HtmlData { get; set; }

        public string HtmlContainerName { get; set; }
    }
}
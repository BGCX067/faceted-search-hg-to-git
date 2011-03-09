using System;
using FacetedSearch.Params;

namespace FacetedSearch.Common
{
    public class ElementVisitedHandler : EventArgs
    {
        public ISearchOptionsParam Element { get; set; }
    }
}
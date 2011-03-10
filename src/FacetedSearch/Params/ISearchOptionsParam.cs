using System;
using FacetedSearch.Common;
using FacetedSearch.SD;

namespace FacetedSearch.Params
{
    public interface ISearchOptionsParam
    {
        string Name { get; set; }
        string Description { get; set; }
        string Help { get; set; }

        int Order { get; set; }
        SearchOptionsParamType ParamType { get; }
        event EventHandler<SearchOptionsParamOrderArgs> OrderChanged;
        ISD GetSD();
    }
}
using System.Collections.Generic;
using FacetedSearch.Params;

namespace FacetedSearch
{
    public interface IJsonSerializer
    {
        string Serialize(IList<ISearchOptionsParam> searchOptionsParams);
    }
}
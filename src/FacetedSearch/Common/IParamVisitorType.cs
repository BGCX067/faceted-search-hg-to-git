using FacetedSearch.Params;

namespace FacetedSearch.Common
{
    public interface IParamVisitorType
    {
        SearchOptionsParamType Type { get; }
    }
}
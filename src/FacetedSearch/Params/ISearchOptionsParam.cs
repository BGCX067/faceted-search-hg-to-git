using FacetedSearch.SD;

namespace FacetedSearch.Params
{
    public interface ISearchOptionsParam
    {
        string Name { get; set; }
        int Order { get; set; }
        ISD GetSD();
    }
}
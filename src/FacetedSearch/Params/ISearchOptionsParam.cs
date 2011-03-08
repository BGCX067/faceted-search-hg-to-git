using FacetedSearch.SD;

namespace FacetedSearch.Params
{
    public interface ISearchOptionsParam
    {
        string Name { get; set; }
        string Description { get; set; }

        int Order { get; set; }
        ISD GetSD();
    }
}
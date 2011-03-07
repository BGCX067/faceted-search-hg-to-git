using FacetedSearch.SD;

namespace FacetedSearch.Params
{
    public class TextSearchOptionsParam : BaseSearchOptionsParam, ISD
    {
        public TextSearchOptionsParam(string searchOptionsName) : base(searchOptionsName)
        {
        }

        public string Text { get; set; }

        public bool IsDisabled { get; set; }
    }
}
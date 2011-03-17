using FacetedSearch.Builder;
using FacetedSearch.Params;

namespace FacetedSearch.Factory
{
    public interface ISearchOptionsParamBuilderFactory
    {
        TextSearchOptionsParamBuilder GetTextParamBuilder(TextSearchOptionsParam textSearchOptionsParam, SearchOptionsBuilder searchOptionsBuilder);
        CheckboxSearchOptionsParamBuilder GetCheckboxParamBuilder(CheckboxSearchOptionsParam checkboxSearchOptionsParam, SearchOptionsBuilder searchOptionsBuilder);
    }
}
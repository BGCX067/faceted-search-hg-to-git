using FacetedSearch.Builder;
using FacetedSearch.Params;

namespace FacetedSearch.Factory
{
    public interface ISearchOptionsParamBuilderFactory<TModel> where TModel : new()
    {
        TextSearchOptionsParamBuilder<TModel> GetTextParamBuilder(TextSearchOptionsParam textSearchOptionsParam, SearchOptionsBuilder<TModel> searchOptionsBuilder);
        CheckboxSearchOptionsParamBuilder<TModel> GetCheckboxParamBuilder(CheckboxSearchOptionsParam checkboxSearchOptionsParam, SearchOptionsBuilder<TModel> searchOptionsBuilder);
    }
}
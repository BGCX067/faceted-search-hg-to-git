using FacetedSearch.Builder;
using FacetedSearch.Params;

namespace FacetedSearch.Factory
{
    public class SearchOptionsParamBuilderBuilderFactory : ISearchOptionsParamBuilderFactory
    {
        #region ISearchOptionsParamBuilderFactory Members

        public TextSearchOptionsParamBuilder GetTextParamBuilder(TextSearchOptionsParam textSearchOptionsParam,
                                                                 SearchOptionsBuilder searchOptionsBuilder)
        {
            return new TextSearchOptionsParamBuilder(textSearchOptionsParam, searchOptionsBuilder);
        }

        public CheckboxSearchOptionsParamBuilder GetCheckboxParamBuilder(
            CheckboxSearchOptionsParam checkboxSearchOptionsParam, SearchOptionsBuilder searchOptionsBuilder)
        {
            return new CheckboxSearchOptionsParamBuilder(checkboxSearchOptionsParam, searchOptionsBuilder);
        }

        #endregion
    }
}
using FacetedSearch.Common;
using FacetedSearch.Params;

namespace FacetedSearch.QueryBuilder.ParamVisitors
{
    public class TextVisitor : IParamVisitor<TextSearchOptionsParam>
    {
        #region IParamVisitor<TextSearchOptionsParam> Members

        public object Visit(TextSearchOptionsParam element)
        {
            if (!element.IsDisabled)
            {
                return element.Text;
            }
            return null;
        }

        public object Visit<T>(T element) where T : class, ISearchOptionsParam
        {
            var el = element as TextSearchOptionsParam;
            return Visit(el);
        }


        public SearchOptionsParamType Type
        {
            get { return SearchOptionsParamType.Text; }
        }

        #endregion
    }
}
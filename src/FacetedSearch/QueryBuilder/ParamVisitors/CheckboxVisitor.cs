namespace FacetedSearch.QueryBuilder.ParamVisitors
{
    using Common;
    using Params;

    public class CheckboxVisitor : IParamVisitor<CheckboxSearchOptionsParam>
    {
        #region IParamVisitor<CheckboxSearchOptionsParam> Members

        public object Visit(CheckboxSearchOptionsParam element)
        {
            if (!element.IsDisabled)
            {
                return element.IsChecked;
            }
            return null;
        }

        public object Visit<T>(T element) where T : class, ISearchOptionsParam
        {
            var el = element as CheckboxSearchOptionsParam;
            return Visit(el);
        }

        public SearchOptionsParamType Type
        {
            get { return SearchOptionsParamType.Checkbox; }
        }

        #endregion
    }
}
using System;
using FacetedSearch.Common;
using FacetedSearch.Params;

namespace FacetedSearch.QueryBuilder
{
    public class CheckboxVisitor : IVisitor<CheckboxSearchOptionsParam>
    {
        public object Visit(CheckboxSearchOptionsParam element)
        {
            throw new NotImplementedException();
        }

        public void Visit<T>(T element) where T : class, ISearchOptionsParam
        {
            var el = element as CheckboxSearchOptionsParam;
            Visit(el);
        }
    }
}
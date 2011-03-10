using System;
using FacetedSearch.Common;
using FacetedSearch.Params;

namespace FacetedSearch.QueryBuilder
{
    public class TextVisitor : IVisitor<TextSearchOptionsParam>
    {
        public object Visit(TextSearchOptionsParam element)
        {
            throw new NotImplementedException();
        }

        public void Visit<T>(T element) where T : class, ISearchOptionsParam
        {
            var el = element as TextSearchOptionsParam;
            Visit(el);
        }
    }
}
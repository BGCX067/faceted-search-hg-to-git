using System;
using System.Linq.Expressions;
using LinqSpecs;

namespace FacetedSearch.QueryBuilder
{
    public class TextSpecification : Specification<ITextSpecification>
    {
        private readonly string _text;

        public TextSpecification(string text)
        {
            _text = text;
        }

        public override Expression<Func<ITextSpecification, bool>> IsSatisfiedBy()
        {
            return c => c.Text == _text;
        }
    }
}
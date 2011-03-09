using FacetedSearch.Params;

namespace FacetedSearch.QueryBuilder
{
    public class TextQueryVisitor : BaseQueryVisitor<TextSearchOptionsParam, ITextSpecification>
    {
        public override void Visit(TextSearchOptionsParam element)
        {
            if (!element.IsDisabled)
            {
                _query = new TextSpecification(element.Text);
            }
        }
    }
}
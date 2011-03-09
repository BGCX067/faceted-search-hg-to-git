namespace FacetedSearch.QueryBuilder
{
    public interface ITextSpecification : ISpecification
    {
        string Text { get; set; }
    }
}
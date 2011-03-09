namespace FacetedSearch.Common
{
    public interface IVisitorElement
    {
        void Accept(IVisitor visitor);
    }
}
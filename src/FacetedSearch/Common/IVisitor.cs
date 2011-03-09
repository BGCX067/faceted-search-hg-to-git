namespace FacetedSearch.Common
{
    public interface IVisitor
    {
        void Visit(object element);
    }

    public interface IVisitor<in T> : IVisitor
    {
        void Visit(T element);
    }
}
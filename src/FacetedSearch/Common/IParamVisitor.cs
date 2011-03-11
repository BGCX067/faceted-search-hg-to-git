namespace FacetedSearch.Common
{
    public interface IParamVisitor<in T> : IVisitor<T>, IParamVisitorType where T : class
    {
    }
}
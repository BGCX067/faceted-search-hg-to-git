namespace FacetedSearch.Mapping
{
    using System.Linq.Expressions;

    public abstract class PropertyMapper
    {
        public abstract BinaryExpression GetCompareExpression(MemberExpression parameter, object userChoice);
    }
}
namespace FacetedSearch.Mapping
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public class SingleValueMapper : PropertyMapper
    {
        public override BinaryExpression GetCompareExpression(MemberExpression propertyExpression, object userSelection)
        {
            PropertyInfo propertyInfo = (PropertyInfo) propertyExpression.Member;
            var propertyValueInDeclatedType = Convert.ChangeType(userSelection, propertyInfo.PropertyType);
            return Expression.Equal(propertyExpression, Expression.Constant(propertyValueInDeclatedType));
        }
    }
}
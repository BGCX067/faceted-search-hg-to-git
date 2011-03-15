namespace FacetedSearch.Mapping
{
    using System;
    using System.Linq.Expressions;

    public class SingleValueMapper : PropertyMapper
    {
        public override BinaryExpression GetCompareExpression(MemberExpression propertyExpression, object userSelection)
        {
            ConstantExpression constantExpression = Expression.Constant(userSelection.ToString());
            if (propertyExpression.Type == typeof (bool))
            {
                constantExpression = Expression.Constant(Convert.ToBoolean(userSelection));
            }

            return Expression.Equal(propertyExpression, constantExpression);
        }
    }
}
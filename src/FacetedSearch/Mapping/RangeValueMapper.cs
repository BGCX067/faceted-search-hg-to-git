namespace FacetedSearch.Mapping
{
    using System;
    using System.Linq.Expressions;

    public class RangeValueMapper : PropertyMapper
    {
        public override BinaryExpression GetCompareExpression(MemberExpression propertyExpression, object userChoice)
        {
            var range = (Tuple<int, int>) userChoice;
            ConstantExpression constantExpressionFrom = Expression.Constant(range.Item1);
            ConstantExpression constantExpressionTo = Expression.Constant(range.Item2);

            return
                Expression.AndAlso(
                    Expression.GreaterThanOrEqual(propertyExpression, constantExpressionFrom),
                    Expression.LessThanOrEqual(propertyExpression, constantExpressionTo));
        }
    }
}
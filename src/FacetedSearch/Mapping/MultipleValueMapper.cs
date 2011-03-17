using System.Collections.Generic;
using System.Linq;

namespace FacetedSearch.Mapping
{
    using System;
    using System.Linq.Expressions;

    public class MultipleValueMapper : PropertyMapper
    {
        public override BinaryExpression GetCompareExpression(MemberExpression propertyExpression, object userChoice)
        {
            var selectedValues = (IList<object>) userChoice;

            BinaryExpression contains = null;
            foreach (var o in selectedValues)
            {
                if (contains == null)
                {
                    contains = Expression.Equal(
                        propertyExpression,
                        Expression.Constant(o)
                        );
                    continue;
                }

                contains = Expression.OrElse(
                    contains,
                    Expression.Equal(
                        propertyExpression,
                        Expression.Constant(o)
                        ));
            }

            return contains;
        }
    }
}
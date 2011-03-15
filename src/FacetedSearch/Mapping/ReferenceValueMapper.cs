namespace FacetedSearch.Mapping
{
    using System;
    using System.Linq.Expressions;

    public class ReferenceValueMapper : PropertyMapper
    {
        public override BinaryExpression GetCompareExpression(MemberExpression propertyExpression, object userChoice)
        {
            throw new NotImplementedException();
        }

        public static PropertyMapper Create(params object[] references)
        {
            if (references.Length%2 != 0)
            {
                throw new ArgumentException();
            }

            return new ReferenceValueMapper();
        }
    }
}
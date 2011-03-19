namespace FacetedSearch.Mapping
{
    using System.Linq.Expressions;
    using System.Reflection;

    internal class PropertyMember
    {
        public PropertyMember(MemberExpression memberExpression)
        {
            MemberExpression = memberExpression;
            PropertyInfo = memberExpression.Member as PropertyInfo;

            if (MemberExpression.Expression.NodeType == ExpressionType.MemberAccess)
            {
                IsMemberAccess = true;
                Parent = new PropertyMember(MemberExpression.Expression as MemberExpression);
            }

            Name = IsMemberAccess ? string.Format("{0}.{1}", Parent.Name, PropertyInfo.Name) : PropertyInfo.Name;
        }

        public PropertyMappingType MappingType { get; set; }

        protected PropertyMember Parent { get; set; }
        protected bool IsMemberAccess { get; set; }
        public MemberExpression MemberExpression { get; private set; }
        public PropertyInfo PropertyInfo { get; set; }
        public string Name { get; set; }

        public MemberExpression AccessExpression(Expression parameter)
        {
            if (!IsMemberAccess)
                return Expression.Property(parameter, PropertyInfo);

            MemberExpression accessExpression = Parent.AccessExpression(parameter);
            return Expression.Property(accessExpression, PropertyInfo);
        }
    }
}
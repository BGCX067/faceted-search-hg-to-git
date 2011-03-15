namespace FacetedSearch.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    public class FacatedSearchMapper
    {
    }

    public class FacatedSearchMapper<T> : FacatedSearchMapper
    {
        private static readonly Dictionary<PropertyMappingType, PropertyMapper> _mappers;
        private readonly Dictionary<MemberInfo, PropertyMappingType> _propertyMappings;

        static FacatedSearchMapper()
        {
            _mappers = new Dictionary<PropertyMappingType, PropertyMapper>();
            _mappers[PropertyMappingType.SingleValue] = new SingleValueMapper();
            _mappers[PropertyMappingType.RangeValue] = new RangeValueMapper();
        }

        public FacatedSearchMapper()
        {
            _propertyMappings = new Dictionary<MemberInfo, PropertyMappingType>();
        }

        public FacatedSearchMapper<T> Set(Expression<Func<T, object>> property,
                                          PropertyMappingType propertyMapper = PropertyMappingType.SingleValue)
        {
            var memberExpression = property.Body as UnaryExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("property");
            }

            var expression = memberExpression.Operand as MemberExpression;
            _propertyMappings.Add(expression.Member, propertyMapper);
            return this;
        }

        public Func<T, bool> Execute(Dictionary<string, object> userChoice)
        {
            Expression finalExpression = null;

            Type @type = typeof (T);
            ParameterExpression parameter = Expression.Parameter(@type, @type.Name.ToLower());
            foreach (var propertyMapping in _propertyMappings)
            {
                string propertyName = propertyMapping.Key.Name;
                object userChoiceForParameter = userChoice[propertyMapping.Key.Name];
                PropertyMapper propertyMapper = _mappers[propertyMapping.Value];

                BinaryExpression expression =
                    propertyMapper.GetCompareExpression(Expression.Property(parameter, propertyName),
                                                        userChoiceForParameter);
                finalExpression = finalExpression == null ? expression : Expression.AndAlso(finalExpression, expression);
            }

            // todo: remove
            //
            Console.WriteLine("Linq expression: {0}", finalExpression);
            return finalExpression != null
                       ? Expression.Lambda<Func<T, bool>>(finalExpression, parameter).Compile()
                       : null;
        }
    }
}
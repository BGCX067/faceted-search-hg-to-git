namespace FacetedSearch.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public class FacatedSearchMapper
    {
    }

    public class FacatedSearchMapper<T> : FacatedSearchMapper
    {
        private static readonly Dictionary<PropertyMappingType, PropertyMapper> _mappers;
        private readonly Dictionary<PropertyMember, PropertyMappingType> _propertyMappings;
        private readonly Dictionary<PropertyMember, PropertyMapper> _propertyReferences;

        static FacatedSearchMapper()
        {
            _mappers = new Dictionary<PropertyMappingType, PropertyMapper>();
            _mappers[PropertyMappingType.SingleValue] = new SingleValueMapper();
            _mappers[PropertyMappingType.RangeValue] = new RangeValueMapper();
            _mappers[PropertyMappingType.ReferenceValue] = new MultipleValueMapper(null);
        }

        public FacatedSearchMapper()
        {
            _propertyMappings = new Dictionary<PropertyMember, PropertyMappingType>();
            _propertyReferences = new Dictionary<PropertyMember, PropertyMapper>();
        }

        public FacatedSearchMapper<T> Property(Expression<Func<T, object>> property,
                                          PropertyMappingType propertyMapper = PropertyMappingType.SingleValue)
        {
            var propertyInfo = GetPropertyInfo(property);
            _propertyMappings.Add(propertyInfo, propertyMapper);

            return this;
        }


        public FacatedSearchMapper<T> Reference(Expression<Func<T, object>> referenceProperty, params object[] keyValueReference)
        {
            if (keyValueReference.Length % 2 != 0)
            {
                throw new ArgumentException("keyValueReference");
            }

            var propertyInfo = GetPropertyInfo(referenceProperty);
            _propertyMappings.Add(propertyInfo, PropertyMappingType.ReferenceValue);

//            var references = new Dictionary<object, object>();
//            for (int i = 0; i < keyValueReference.Length; i+=2)
//            {
//                references.Add(keyValueReference[i], keyValueReference[i+1]);
//            }
//            
//            _propertyReferences.Add(propertyInfo, new ReferenceValueMapper(references));
            return this;
        }

        private PropertyMember GetPropertyInfo(Expression<Func<T, object>> property)
        {
            MemberExpression memberExpression = null;
            if (property.Body is MemberExpression)
            {
                memberExpression = property.Body as MemberExpression;
            }
            else
            {
                if (!(property.Body is UnaryExpression))
                {
                    throw new ArgumentException("property");
                }
                memberExpression = (property.Body as UnaryExpression).Operand as MemberExpression;
            }

            return new PropertyMember(memberExpression);
        }

        public Func<T, bool> Execute(Dictionary<string, object> userChoice)
        {
            Expression finalExpression = null;

            Type @type = typeof (T);
            ParameterExpression parameter = Expression.Parameter(@type, @type.Name.ToLower());
            foreach (var propertyMapping in _propertyMappings)
            {
                PropertyMember propertyMember = propertyMapping.Key;
                object userChoiceForParameter = userChoice[propertyMember.Name];
                PropertyMapper propertyMapper = _mappers[propertyMapping.Value];

                BinaryExpression expression =
                    propertyMapper.GetCompareExpression(propertyMember.AccessExpression(parameter),
                                                        userChoiceForParameter);

                finalExpression = finalExpression == null
                                      ? expression
                                      : Expression.AndAlso(finalExpression, expression);
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
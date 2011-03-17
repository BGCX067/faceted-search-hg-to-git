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
            _mappers[PropertyMappingType.MultipleValue] = new MultipleValueMapper();
        }

        public FacatedSearchMapper()
        {
            _propertyMappings = new Dictionary<PropertyMember, PropertyMappingType>();
            _propertyReferences = new Dictionary<PropertyMember, PropertyMapper>();
        }

        public FacatedSearchMapper<T> Property<TProperty>(
            Expression<Func<T, TProperty>> property,
            PropertyMappingType propertyMappingType = PropertyMappingType.SingleValue)
        {
            PropertyMember propertyInfo = GetPropertyInfo(property);
            _propertyMappings.Add(propertyInfo, propertyMappingType);

            return this;
        }

        public FacatedSearchMapper<T> Range<TProperty>(Expression<Func<T, TProperty>> rangeProperty)
        {
            return Property(rangeProperty, PropertyMappingType.RangeValue);
        }

        public FacatedSearchMapper<T> List<TProperty>(Expression<Func<T, TProperty>> listProperty)
        {
            return Property(listProperty, PropertyMappingType.MultipleValue);
        }

        private PropertyMember GetPropertyInfo<TProperty>(Expression<Func<T, TProperty>> property)
        {
            if(!(property.Body is MemberExpression))
            {
                throw new ApplicationException("property");
            }

            return new PropertyMember(property.Body as MemberExpression);
        }

        public Func<T, bool> Execute(Dictionary<string, object> userChoice)
        {
            Expression finalExpression = null;

            Type @type = typeof (T);
            ParameterExpression parameter = Expression.Parameter(@type, @type.Name.ToLower());
            foreach (var propertyMapping in _propertyMappings)
            {
                if(!userChoice.Keys.Contains(propertyMapping.Key.Name)) continue;

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
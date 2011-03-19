namespace FacetedSearch.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class FacatedSearchMapper
    {
    }

    public class FacatedSearchMapper<T> : FacatedSearchMapper
    {
        private static readonly Dictionary<PropertyMappingType, PropertyMapper> _mappers;
        private readonly Dictionary<string, PropertyMember> _propertyMappings;

        static FacatedSearchMapper()
        {
            _mappers = new Dictionary<PropertyMappingType, PropertyMapper>();
            _mappers[PropertyMappingType.SingleValue] = new SingleValueMapper();
            _mappers[PropertyMappingType.RangeValue] = new RangeValueMapper();
            _mappers[PropertyMappingType.MultipleValue] = new MultipleValueMapper();
        }

        public FacatedSearchMapper()
        {
            _propertyMappings = new Dictionary<string, PropertyMember>();
        }

        public FacatedSearchMapper<T> Property<TProperty>(
            Expression<Func<T, TProperty>> property,
            string referenceName = null)
        {
            return Property(property, referenceName, PropertyMappingType.SingleValue);
        }

        private FacatedSearchMapper<T> Property<TProperty>(
            Expression<Func<T, TProperty>> property,
            string referenceName = null,
            PropertyMappingType propertyMappingType = PropertyMappingType.SingleValue)
        {
            PropertyMember propertyInfo = GetPropertyInfo(property);
            propertyInfo.MappingType = propertyMappingType;
            _propertyMappings.Add(referenceName ?? propertyInfo.Name, propertyInfo);

            return this;
        }

        public FacatedSearchMapper<T> Range<TProperty>(Expression<Func<T, TProperty>> rangeProperty,
                                                       string referenceName = null)
        {
            return Property(rangeProperty, referenceName, PropertyMappingType.RangeValue);
        }

        public FacatedSearchMapper<T> List<TProperty>(Expression<Func<T, TProperty>> listProperty,
                                                      string referenceName = null)
        {
            return Property(listProperty, referenceName, PropertyMappingType.MultipleValue);
        }

        private PropertyMember GetPropertyInfo<TProperty>(Expression<Func<T, TProperty>> property)
        {
            if (!(property.Body is MemberExpression))
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
                if (!userChoice.Keys.Contains(propertyMapping.Key)) continue;

                PropertyMember propertyMember = propertyMapping.Value;
                object userChoiceForParameter = userChoice[propertyMapping.Key];
                PropertyMapper propertyMapper = _mappers[propertyMember.MappingType];

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
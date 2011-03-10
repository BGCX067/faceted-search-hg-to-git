using System;
using System.Collections.Generic;
using FacetedSearch.Common;
using FacetedSearch.Params;
using Lokad;

namespace FacetedSearch.QueryBuilder
{
    public class DictionaryQueryVisitor : IVisitor, IBuilder<IDictionary<string, object>>
    {
        private readonly IDictionary<string, object> _values = new Dictionary<string, object>();

        private readonly Func<ISearchOptionsParam, Pair<bool, object>> _visitorMapper =
            _ => new Pair<bool, object>(false, null);

        private static readonly Dictionary<SearchOptionsParamType, IVisitor> InternalMap;

        static DictionaryQueryVisitor()
        {
            InternalMap = new Dictionary<SearchOptionsParamType, IVisitor> {{SearchOptionsParamType.Text, new TextVisitor()}};
            InternalMap = new Dictionary<SearchOptionsParamType, IVisitor> { { SearchOptionsParamType.Checkbox, new CheckboxVisitor() } };
        }

        public DictionaryQueryVisitor()
        {
        }

        public DictionaryQueryVisitor(Func<ISearchOptionsParam, Pair<bool, object>> visitorMapper)
        {
            _visitorMapper = visitorMapper;
        }

        #region IBuilder<IDictionary<string,object>> Members

        public IBuilder<IDictionary<string, object>> BuildPart(object part)
        {
            var param = part as ISearchOptionsParam;
            if (param == null)
            {
                return this;
            }

            var visitorMapperResult = _visitorMapper(param);
            var paramValues = visitorMapperResult.Key ? visitorMapperResult.Value : InternalMapper(param);

            _values.Add(param.Name, paramValues);
            return this;
        }


        public IDictionary<string, object> GetResult()
        {
            return _values;
        }

        #endregion

        #region IVisitor Members

        public void Visit<T>(T element) where T : class, ISearchOptionsParam
        {
            BuildPart(element);
        }

        #endregion

        private static object InternalMapper<T>(T param) where T : class, ISearchOptionsParam
        {
            var visitor = InternalMap[param.ParamType] as IVisitor<T>;
            return visitor == null ? null : visitor.Visit(param);
        }
    }
}
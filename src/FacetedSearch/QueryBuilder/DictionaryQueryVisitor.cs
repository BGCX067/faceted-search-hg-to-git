namespace FacetedSearch.QueryBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Common;
    using Lokad;
    using Params;

    public class DictionaryQueryVisitor : IVisitor, IBuilder<IDictionary<string, object>>
    {
        protected static readonly IDictionary<SearchOptionsParamType, IVisitor> InternalMap =
            new Dictionary<SearchOptionsParamType, IVisitor>();

        private readonly IDictionary<string, object> _values = new Dictionary<string, object>();

        private readonly Func<ISearchOptionsParam, Pair<bool, object>> _visitorMapper =
            _ => new Pair<bool, object>(false, null);

        static DictionaryQueryVisitor()
        {
            //automatically register all intermal supported types
            Assembly.GetAssembly(typeof (IParamVisitor<>)).GetExportedTypes().Where(
                _ => _.GetInterfaces().Any(x => x == typeof (IParamVisitor<>))).ForEach(delegate(Type type)
                                                                                            {
                                                                                                var visitor =
                                                                                                    (IParamVisitorType)
                                                                                                    Activator.
                                                                                                        CreateInstance(
                                                                                                            type);
                                                                                                InternalMap.Add(
                                                                                                    visitor.Type,
                                                                                                    (IVisitor) visitor);
                                                                                            });
        }

        public DictionaryQueryVisitor()
        {
        }

        public DictionaryQueryVisitor(Func<ISearchOptionsParam, Pair<bool, object>> visitorMapper) : this()
        {
            if (visitorMapper != null)
            {
                _visitorMapper = visitorMapper;
            }
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

        public object Visit<T>(T element) where T : class, ISearchOptionsParam
        {
            return BuildPart(element);
        }

        #endregion

        private static object InternalMapper<T>(T param) where T : class, ISearchOptionsParam
        {
            var visitor = InternalMap[param.ParamType] as IVisitor<T>;
            return visitor == null ? null : visitor.Visit(param);
        }
    }
}
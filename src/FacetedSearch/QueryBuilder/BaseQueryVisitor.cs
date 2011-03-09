using System;
using System.Linq.Expressions;
using FacetedSearch.Common;
using FacetedSearch.Params;
using LinqSpecs;

namespace FacetedSearch.QueryBuilder
{
    public class BaseQueryVisitor<T, TModel> : IVisitor<T>
        where T : BaseSearchOptionsParam
        where TModel : class, ISpecification
    {
        protected Specification<TModel> _query;

        #region IVisitor<T> Members

        public virtual void Visit(T element)
        {
        }

        void IVisitor.Visit(object element)
        {
            Visit((T) element);
        }

        #endregion

        public Specification<TModel> Specification
        {
            get { return _query; }
        }
    }
}
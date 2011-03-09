using System;
using System.Linq.Expressions;
using FacetedSearch.Common;
using FacetedSearch.Params;
using LinqSpecs;

namespace FacetedSearch.QueryBuilder
{
    public class EventDrivenQueryVisitor : IVisitor
    {
        public event EventHandler<ElementVisitedHandler> ElementVisited;

        public void InvokeElementVisited(ElementVisitedHandler e)
        {
            EventHandler<ElementVisitedHandler> handler = ElementVisited;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #region IVisitor<T> Members

        public void Visit<T>(T element) where T : class, ISearchOptionsParam
        {
            InvokeElementVisited(new ElementVisitedHandler
                                     {
                                         Element = element,
                                     });
        }

        #endregion
    }
}
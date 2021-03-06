﻿namespace FacetedSearch.QueryBuilder
{
    using System;
    using Common;
    using Params;

    public class EventDrivenQueryVisitor : IVisitor
    {
        #region IVisitor Members

        public object Visit<T>(T element) where T : class, ISearchOptionsParam
        {
            InvokeElementVisited(new ElementVisitedHandler
                                     {
                                         Element = element,
                                     });
            return this;
        }

        #endregion

        public event EventHandler<ElementVisitedHandler> ElementVisited;

        public void InvokeElementVisited(ElementVisitedHandler e)
        {
            EventHandler<ElementVisitedHandler> handler = ElementVisited;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
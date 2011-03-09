using System;
using FacetedSearch.Common;
using FacetedSearch.SD;
using Lokad;

namespace FacetedSearch.Params
{
    public abstract class BaseSearchOptionsParam : ISearchOptionsParam, IVisitorElement
    {
        private string _name;
        private int _order = -1;

        protected BaseSearchOptionsParam()
        {
        }

        protected BaseSearchOptionsParam(string name)
        {
            _name = name;
        }

        #region ISearchOptionsParam Members

        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description { get; set; }

        public string Help { get; set; }

        public int Order
        {
            get { return _order; }
            set
            {
                if (_order != value)
                {
                    InvokeOrderChanged(new SearchOptionsParamOrderArgs {OldOrder = _order, NewOrder = value});
                }
                _order = value;
            }
        }

        public event EventHandler<SearchOptionsParamOrderArgs> OrderChanged;

        public virtual ISD GetSD()
        {
            var sd = this as ISD;
            Enforce.Argument(() => sd);

            return sd;
        }

        #endregion

        #region IVisitorElement Members

        void IVisitorElement.Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        #endregion

        protected void InvokeOrderChanged(SearchOptionsParamOrderArgs e)
        {
            EventHandler<SearchOptionsParamOrderArgs> handler = OrderChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
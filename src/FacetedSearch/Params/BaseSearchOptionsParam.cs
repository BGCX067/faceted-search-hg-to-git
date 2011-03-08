using FacetedSearch.SD;
using Lokad;

namespace FacetedSearch.Params
{
    public abstract class BaseSearchOptionsParam : ISearchOptionsParam
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

        public int Order
        {
            get { return _order; }
            set { _order = value; }
        }

        public virtual ISD GetSD()
        {
            var sd = this as ISD;
            Enforce.Argument(() => sd);

            return sd;
        }

        #endregion
    }
}
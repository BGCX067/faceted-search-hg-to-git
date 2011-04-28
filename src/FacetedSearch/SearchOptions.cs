namespace FacetedSearch
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Common;
    using Mapping;
    using Params;
    using SD;

    public class SearchOptions<TModel> : SearchOptions
    {
        public FacatedSearchMapper<TModel> QueryMapper { get; set; }
    }

    public class SearchOptions : ISearchOptions, IVisitorElement
    {
        private readonly IDictionary<int, ISearchOptionsParam> _params = new SortedList<int, ISearchOptionsParam>();

        #region IVisitorElement Members

        public void Accept(IVisitor visitor)
        {
            //visitor.Visit(this);
        }

        #endregion

        public SearchOptionsSD GetSDObject()
        {
            var searchOptionsSD = new SearchOptionsSD
                                      {
                                          Items = _params.Values.Select(_ => _.GetSD()).ToList(),
                                      };


            return searchOptionsSD;
        }

        public ReadOnlyCollection<ISearchOptionsParam> GetParams()
        {
            return _params.Values.ToList().AsReadOnly();
        }

        public ISearchOptionsParam AddParam(ISearchOptionsParam param, int indexOrder)
        {
            param.Order = indexOrder;
            return AddParam(param);
        }

        public ISearchOptionsParam AddParam(ISearchOptionsParam param)
        {
            int key;
            var keys = _params.Keys;
            if (keys.Count == 0)
            {
                key = 0;
            }
            else
            {
                key = param.Order < 0 ? keys.Max() + 1 : param.Order;
            }
            param.Order = key;
            param.OrderChanged += OnParamOrderChanged;
            _params.Add(key, param);
            return param;
        }

        private void OnParamOrderChanged(object sender, SearchOptionsParamOrderArgs e)
        {
            var oldOrder = e.OldOrder;
            if (_params.ContainsKey(oldOrder))
            {
                var param = _params[oldOrder];
                _params.Remove(oldOrder);
                _params.Add(e.NewOrder, param);
            }
        }
    }
}
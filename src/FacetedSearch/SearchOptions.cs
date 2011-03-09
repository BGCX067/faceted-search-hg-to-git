using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FacetedSearch.Common;
using FacetedSearch.Params;
using FacetedSearch.SD;

namespace FacetedSearch
{
    public class SearchOptions : ISearchOptions
    {
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IDictionary<int, ISearchOptionsParam> _params = new SortedList<int, ISearchOptionsParam>();

        public SearchOptions(IJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        public string HtmlData { get; set; }
        public string HtmlContainerName { get; set; }

        #region ISearchOptions Members

        public string GetJson()
        {
            var searchOptionsSD = new SearchOptionsSD
                                      {
                                          Items = _params.Values.Select(_ => _.GetSD()).ToList(),
                                          HtmlData = HtmlData,
                                          HtmlContainerName = HtmlContainerName,
                                      };


            var paramsPart = _jsonSerializer.Serialize(searchOptionsSD);

            //add extra options to json, think about versioning

            return paramsPart;
        }

        #endregion

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
using System;

namespace FacetedSearch.Common
{
    public class SearchOptionsParamOrderArgs : EventArgs
    {
        public int OldOrder { get; set; }

        public int NewOrder { get; set; }
    }
}
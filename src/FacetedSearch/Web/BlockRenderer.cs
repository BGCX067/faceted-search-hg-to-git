namespace FacetedSearch.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;

    public class BlockRenderer : Queue<TagBuilder>
    {
        public string Render(Func<Queue<TagBuilder>, Queue<TagBuilder>> modifier = null)
        {
            var queue = modifier != null ? modifier(this) : this;
            return queue.Aggregate(new StringBuilder(), (sb, tb) => sb.Append(tb.ToString())).ToString();
        }
    }
}
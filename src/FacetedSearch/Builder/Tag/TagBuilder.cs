using System.Web.Mvc;
using System.Web.UI;

namespace FacetedSearch.Builder.Tag
{
    public class HtmlTagBuilder : TagBuilder
    {
        private readonly HtmlTextWriterTag _tagType;

        public HtmlTagBuilder(HtmlTextWriterTag tagType)
            : base(tagType.ToString())
        {
            _tagType = tagType;
        }

        public HtmlTextWriterTag TagType
        {
            get { return _tagType; }
        }
    }
}
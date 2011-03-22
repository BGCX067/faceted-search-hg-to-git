namespace FacetedSearch.Builder.Tag
{
    using System.Web.UI;

    public class LabelTagBuilder : HtmlTagBaseBuilder<LabelTagBuilder>
    {
        public LabelTagBuilder()
            : base(HtmlTextWriterTag.Label)
        {
        }

        public LabelTagBuilder For(string inputName)
        {
            SetAttribute(HtmlTextWriterAttribute.For, inputName);
            return this;
        }
    }
}
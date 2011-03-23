namespace FacetedSearch.Builder.Tag
{
    using System.Web.Mvc;
    using System.Web.UI;
    using Lokad;

    public class HtmlTagBaseBuilder<THtmlTag> where THtmlTag : HtmlTagBaseBuilder<THtmlTag>
    {
        protected HtmlTagBaseBuilder(HtmlTextWriterTag tag)
        {
            HtmlTagBuilder = new HtmlTagBuilder(tag);
        }

        public HtmlTagBuilder HtmlTagBuilder { get; protected set; }

        public TagBuilder TagBuilder
        {
            get { return HtmlTagBuilder.ToTagBuilder(); }
        }

        protected virtual TagRenderMode RenderMode
        {
            get { return TagRenderMode.Normal; }
        }

        protected virtual THtmlTag SetAttribute(HtmlTextWriterAttribute attribute, object value)
        {
            Enforce.Argument(() => value);
            HtmlTagBuilder.MergeAttribute(attribute, value);
            return (THtmlTag) this;
        }

        public virtual THtmlTag InnerText(string value)
        {
            Enforce.Argument(() => value);
            Enforce.That(RenderMode != TagRenderMode.SelfClosing);

            HtmlTagBuilder.SetInnerText(value);
            return (THtmlTag) this;
        }

        public THtmlTag Id(string id)
        {
            SetAttribute(HtmlTextWriterAttribute.Id, id);
            return (THtmlTag) this;
        }

        public THtmlTag Name(string name)
        {
            SetAttribute(HtmlTextWriterAttribute.Name, name);
            return (THtmlTag) this;
        }

        public override string ToString()
        {
            return HtmlTagBuilder.ToString(RenderMode);
        }
    }
}
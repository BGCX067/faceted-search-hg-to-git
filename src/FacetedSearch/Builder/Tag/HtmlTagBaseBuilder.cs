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

        protected virtual THtmlTag SetStyle(HtmlTextWriterStyle style, object value)
        {
            Enforce.Argument(() => value);
            HtmlTagBuilder.MergeStyle(style, value);
            return (THtmlTag) this;
        }

        protected virtual THtmlTag SetCssClass(string cssClass, bool isOverwrite = false)
        {
            Enforce.ArgumentNotEmpty(() => cssClass);

            var resultClass = string.Empty;
            if (isOverwrite)
            {
                resultClass = cssClass;
            }
            else
            {
                var sourceCssClass =
                    HtmlTagBuilder.Attributes.GetValue(HtmlTextWriterAttribute.Class, string.Empty).ToString();
                int startInd;
                if (!((startInd = sourceCssClass.IndexOf(cssClass)) >= 0 &&
                    (
                    //end of string
                        (startInd + cssClass.Length >= sourceCssClass.Length)
                        ||
                    // or inside string
                        (char.IsWhiteSpace(sourceCssClass, startInd + cssClass.Length))
                    )
                    ))
                {
                    resultClass = sourceCssClass + (string.IsNullOrWhiteSpace(sourceCssClass) ? "" : " ") + cssClass;
                }
            }

            if (!string.IsNullOrEmpty(resultClass))
            {
                SetAttribute(HtmlTextWriterAttribute.Class, resultClass);
            }

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
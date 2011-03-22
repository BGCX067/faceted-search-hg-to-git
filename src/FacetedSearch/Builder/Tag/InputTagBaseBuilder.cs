namespace FacetedSearch.Builder.Tag
{
    using System.Web.Mvc;
    using System.Web.UI;

    public abstract class InputTagBaseBuilder<TInputTagBuilder> : HtmlTagBaseBuilder<TInputTagBuilder>
        where TInputTagBuilder : InputTagBaseBuilder<TInputTagBuilder>
    {
        protected InputTagBaseBuilder() : base(HtmlTextWriterTag.Input)
        {
// ReSharper disable DoNotCallOverridableMethodsInConstructor
            SetAttribute(HtmlTextWriterAttribute.Type, Type);
// ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        protected abstract HtmlTextWriterTypeAttribute Type { get; }

        protected override TagRenderMode RenderMode
        {
            get { return TagRenderMode.SelfClosing; }
        }

        public TInputTagBuilder Disabled(bool isDisabled)
        {
            if (isDisabled)
            {
                SetAttribute(HtmlTextWriterAttribute.Disabled, HtmlTextWriterAttribute.Disabled);
            }

            return (TInputTagBuilder) this;
        }

        public TInputTagBuilder Value(object value)
        {
            SetAttribute(HtmlTextWriterAttribute.Value, value);
            return (TInputTagBuilder) this;
        }
    }
}
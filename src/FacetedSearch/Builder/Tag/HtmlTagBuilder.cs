namespace FacetedSearch.Builder.Tag
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.UI;
    using Lokad;

    public class HtmlTagBuilder
    {
        private readonly HtmlTextWriterTag _tagType;
        private string _innerHtml;

        public HtmlTagBuilder(HtmlTextWriterTag tagType)
        {
            _tagType = tagType;
            Attributes = new SortedDictionary<HtmlTextWriterAttribute, object>();
            StyleValues = new Dictionary<HtmlTextWriterStyle, object>();
        }

        public HtmlTextWriterTag TagType
        {
            get { return _tagType; }
        }

        public IDictionary<HtmlTextWriterAttribute, object> Attributes { get; private set; }

        public string InnerHtml
        {
            get { return _innerHtml ?? String.Empty; }
            set { _innerHtml = value; }
        }

        public IDictionary<HtmlTextWriterStyle, object> StyleValues { get; private set; }

        public HtmlTagBuilder MergeStyle(HtmlTextWriterStyle style, object value,
                                         bool replaceExisting = false)
        {
            Enforce.Argument(() => value);
            if (replaceExisting || !StyleValues.ContainsKey(style))
            {
                StyleValues[style] = value;
            }
            return this;
        }

        public HtmlTagBuilder MergeAttribute(HtmlTextWriterAttribute attribute, object value,
                                             bool replaceExisting = false)
        {
            Enforce.Argument(() => value);
            if (replaceExisting || !Attributes.ContainsKey(attribute))
            {
                Attributes[attribute] = value;
            }
            return this;
        }

        public HtmlTagBuilder MergeAttributes<TValue>(IDictionary<HtmlTextWriterAttribute, TValue> attributes,
                                                      bool replaceExisting = false)
        {
            if (attributes != null)
            {
                attributes.ForEach(_ => MergeAttribute(_.Key, _.Value, replaceExisting));
            }
            return this;
        }

        public HtmlTagBuilder SetInnerText(string innerText)
        {
            InnerHtml = HttpUtility.HtmlEncode(innerText);
            return this;
        }

        public void SetCssClass(string cssClass)
        {
            object currentValue;

            if (!Attributes.TryGetValue(HtmlTextWriterAttribute.Class, out currentValue))
            {
                var currentCssClass = currentValue.ToString();
                if (!currentCssClass.Contains(cssClass))
                {
                    Attributes[HtmlTextWriterAttribute.Class] = string.Concat(cssClass, " ", currentValue);
                }
            }
            else
            {
                Attributes[HtmlTextWriterAttribute.Class] = cssClass;
            }
        }

        public override string ToString()
        {
            return ToString(TagRenderMode.Normal);
        }

        private void AppendAttributes(StringBuilder sb)
        {
            foreach (var attribute in Attributes)
            {
                string value = HttpUtility.HtmlAttributeEncode(attribute.Value.ToString());

                if (attribute.Key == HtmlTextWriterAttribute.Style)
                {
                    //replace style attribute value
                    if (StyleValues.Count > 0)
                    {
                        value = ComposeStyleAttributeValue();
                    }
                }

                sb.Append(' ')
                    .Append(attribute.Key)
                    .Append("=\"")
                    .Append(value)
                    .Append("\"");
            }
        }

        private string ComposeStyleAttributeValue()
        {
            var sbStyle = new StringBuilder();
            StyleValues.ForEach(
                _ =>
                sbStyle.Append(_.Key)
                    .Append(':')
                    .Append(HttpUtility.HtmlAttributeEncode(_.Value.ToString()))
                    .Append(';'));
            return sbStyle.ToString();
        }

        public string ToString(TagRenderMode renderMode)
        {
            var tagName = TagType.ToString();
            var sb = new StringBuilder();
            switch (renderMode)
            {
                case TagRenderMode.StartTag:
                    sb.Append('<')
                        .Append(tagName);
                    AppendAttributes(sb);
                    sb.Append('>');
                    break;
                case TagRenderMode.EndTag:
                    sb.Append("</")
                        .Append(tagName)
                        .Append('>');
                    break;
                case TagRenderMode.SelfClosing:
                    sb.Append('<')
                        .Append(tagName);
                    AppendAttributes(sb);
                    sb.Append(" />");
                    break;
                default:
                    sb.Append('<')
                        .Append(tagName);
                    AppendAttributes(sb);
                    sb.Append('>')
                        .Append(InnerHtml)
                        .Append("</")
                        .Append(tagName)
                        .Append('>');
                    break;
            }
            return sb.ToString();
        }

        public TagBuilder ToTagBuilder()
        {
            var tagBuilder = new TagBuilder(_tagType.ToString()) {InnerHtml = InnerHtml};

            tagBuilder.MergeAttributes(Attributes);
            //replace style attribute value
            if (StyleValues.Count > 0)
            {
                tagBuilder.MergeAttribute(HtmlTextWriterAttribute.Style.ToString(), ComposeStyleAttributeValue());
            }

            return tagBuilder;
        }
    }
}
namespace FacetedSearch.Builder.Tag
{
    public class TextboxTagBuilder : InputTagBaseBuilder<TextboxTagBuilder>
    {
        protected override HtmlTextWriterTypeAttribute Type
        {
            get { return HtmlTextWriterTypeAttribute.text; }
        }
    }
}
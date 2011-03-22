namespace FacetedSearch.Builder.Tag
{
    using System.Web.UI;

    public class CheckboxTagBuilder : InputTagBaseBuilder<CheckboxTagBuilder>
    {
        protected override HtmlTextWriterTypeAttribute Type
        {
            get { return HtmlTextWriterTypeAttribute.checkbox; }
        }

        public CheckboxTagBuilder Checked(bool isChecked)
        {
            if (isChecked)
            {
                SetAttribute(HtmlTextWriterAttribute.Checked, HtmlTextWriterAttribute.Checked);
            }
            return this;
        }
    }
}
namespace FacetedSearch.Web
{
    using System.Collections.Generic;
    using Builder.Tag;
    using Params;

    public class Html
    {
        public static List<BlockRenderer> RenderSearchOptions(SearchOptions searchOptions)
        {
            var blocks = new List<BlockRenderer>();
            foreach (var item in searchOptions.GetParams())
            {
                switch (item.ParamType)
                {
                    case SearchOptionsParamType.Text:
                        blocks.Add(RenderTextbox((TextSearchOptionsParam) item));
                        break;
                    case SearchOptionsParamType.Checkbox:
                        blocks.Add(RenderCheckbox((CheckboxSearchOptionsParam) item));
                        break;
                }
            }

            return blocks;
        }

        public static BlockRenderer RenderCheckbox(CheckboxSearchOptionsParam checkboxSearchOptionsParam)
        {
            var blockRenderer = new BlockRenderer();

            //order is valuable!
            blockRenderer.Enqueue((new CheckboxTagBuilder()
                                      .Disabled(checkboxSearchOptionsParam.IsDisabled)
                                      .Id(checkboxSearchOptionsParam.Name)
                                      .Name(checkboxSearchOptionsParam.Name))
                                      .Value(checkboxSearchOptionsParam.Value)
                                      .Checked(checkboxSearchOptionsParam.IsChecked)
                                      .TagBuilder);
            blockRenderer.Enqueue(new LabelTagBuilder()
                                      .InnerText(checkboxSearchOptionsParam.Description)
                                      .For(checkboxSearchOptionsParam.Name).TagBuilder);


            return blockRenderer;
        }

        public static BlockRenderer RenderTextbox(TextSearchOptionsParam textSearchOptionsParam)
        {
            var blockRenderer = new BlockRenderer();

            //order is valuable!
            blockRenderer.Enqueue((new TextboxTagBuilder()
                                      .Disabled(textSearchOptionsParam.IsDisabled)
                                      .Id(textSearchOptionsParam.Name)
                                      .Name(textSearchOptionsParam.Name))
                                      .Value(textSearchOptionsParam.Text)
                                      .TagBuilder);
            blockRenderer.Enqueue(new LabelTagBuilder()
                                      .InnerText(textSearchOptionsParam.Description)
                                      .For(textSearchOptionsParam.Name).TagBuilder);


            return blockRenderer;
        }
    }
}
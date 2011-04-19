namespace FacetedSearch.Web.Controllers
{
    using System;
    using System.IO;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Builder;
    using Models;
    using Params;
    using SD;

    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            var urlHelper = GetUrlHelper();


            var viewModel = new HomeViewModel
                                {
                                    SearchOptions = GetSearchOptions(),
                                };
            return View(viewModel);
        }

        private UrlHelper GetUrlHelper()
        {
            var htmlHelper = new HtmlHelper(
                new ViewContext(ControllerContext,
                                new WebFormView(ControllerContext, "omg"),
                                new ViewDataDictionary(),
                                new TempDataDictionary(),
                                new StringWriter()),
                new ViewPage(),
                RouteTable.Routes);

            return new UrlHelper(ControllerContext.RequestContext);
        }

        public SearchOptions GetSearchOptions()
        {
            return
                FluentSearchOptions.Configure<Person>()
                        .Text("Name").MapQuery(x => x.Name).End()
                        .Text("Surname").Watermark("Please Enter Surname").MapQuery(x => x.Surname).End()
                    .BuildSearchOptions();
        }

        [HttpPost]
        public ActionResult Search(SearchOptionsSD json)
        {
            var urlHelper = GetUrlHelper();
            var searchOptions = GetSearchOptions();
            ((TextSearchOptionsParam) searchOptions.GetParams()[0]).Text = "new text";

            return Content(searchOptions.GetJson());
        }
    }
}
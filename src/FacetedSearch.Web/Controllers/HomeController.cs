namespace FacetedSearch.Web.Controllers
{
    using System;
    using System.IO;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Builder;
    using Models;

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
                FluentSearchOptions.Configure<HomeController>().Text("Name").MapQuery(
                    x => x.ViewBag).End()
                    .BuildSearchOptions();
        }

        [HttpPost]
        public ActionResult Search(string values)
        {
            var urlHelper = GetUrlHelper();
            var searchOptions = GetSearchOptions();

            return Content(searchOptions.GetJson());
        }
    }
}
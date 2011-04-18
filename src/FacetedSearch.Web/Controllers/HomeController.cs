namespace FacetedSearch.Web.Controllers
{
    using System;
    using System.IO;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Services.Protocols;
    using Builder;
    using Models;
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
                                    SearchOptions = GetSearchOptions("http://localhost:1134/Home/Search"),
                                };
            return View(viewModel);
        }

        private UrlHelper GetUrlHelper()
        {
            var htmlHelper =  new HtmlHelper(
                new ViewContext(ControllerContext, 
                                new WebFormView(ControllerContext, "omg"),
                                new ViewDataDictionary(), 
                                new TempDataDictionary(), 
                                new StringWriter()),
                new ViewPage(),
                RouteTable.Routes);

            return new UrlHelper(ControllerContext.RequestContext);
        }

        public SearchOptions GetSearchOptions(string url)
        {
            return 
            FluentSearchOptions.Configure<HomeController>().Text("Name").MapQuery(
                x => x.ViewBag).End().Url(
                    new Uri(url)).
                BuildSearchOptions();
        }

        [HttpPost]
        public ActionResult Search(FormCollection values)
        {
            var urlHelper = GetUrlHelper();
            var searchOptions = GetSearchOptions("http://localhost:1134/Home/Search");

            return Content(searchOptions.GetJson());
        }
    }
}
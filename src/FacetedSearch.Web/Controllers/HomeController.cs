namespace FacetedSearch.Web.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Builder;
    using Factory;
    using Models;
    using OutputFormatter;
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

        public SearchOptions<Person> GetSearchOptions()
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
            var searchOptions = GetSearchOptions();
            var jsonSearchOptionsFactory = new JsonSearchOptionsFactory();
            var options = jsonSearchOptionsFactory.GetSearchOptions(searchOptions, json);

            new PersonRepository().GetAll().Where(options.QueryMapper.Execute(null));
            ((TextSearchOptionsParam) searchOptions.GetParams()[0]).Text = "new text";

            string resultJson = new JsonFormatter().GetJson(searchOptions, "result", "result");

            return Content(resultJson);
        }
    }
}
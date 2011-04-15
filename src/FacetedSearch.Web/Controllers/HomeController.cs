namespace FacetedSearch.Web.Controllers
{
    using System.Web.Mvc;
    using Builder;
    using Models;

    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            var viewModel = new HomeViewModel
                                {
                                    SearchOptions =
                                        FluentSearchOptions.Configure<HomeController>().Text("Name").MapQuery(
                                            x => x.ViewBag).End().BuildSearchOptions()
                                };
            return View(viewModel);
        }
    }
}
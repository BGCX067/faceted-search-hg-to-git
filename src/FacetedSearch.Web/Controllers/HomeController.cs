using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FacetedSearch.Web.Models;

namespace FacetedSearch.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            var viewModel = new HomeViewModel();
            return View(viewModel);
        }

    }
}

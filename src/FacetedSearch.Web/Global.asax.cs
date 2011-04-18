using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace FacetedSearch.Web
{
    using System.Web.Mvc;
    using Params;
    using SD;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            var valueFactories = new ValueProviderFactory[ValueProviderFactories.Factories.Count];
            ValueProviderFactories.Factories.CopyTo(valueFactories, 0);
            ValueProviderFactories.Factories.Clear();

            ValueProviderFactories.Factories.Add(new DataContractValueProviderFactory(typeof(SearchOptionsSD), new[] {typeof(TextSearchOptionsParam)}));
        }
    }
}
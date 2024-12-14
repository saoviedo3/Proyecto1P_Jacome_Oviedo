using System.Web.Mvc;
using System.Web.Routing;

namespace WebApp.SecurityGUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            // Ruta adicional para categorías (opcional si deseas personalizar).
            routes.MapRoute(
                name: "Categories",
                url: "Categories/{action}/{id}",
                defaults: new { controller = "Categories", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

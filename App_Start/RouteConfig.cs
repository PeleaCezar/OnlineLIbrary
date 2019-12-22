using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineLibrary1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Ruta de rezolvare a crearii unui produs nou
            routes.MapRoute(
                name: "CreateProducts",
                url: "Products/Create",
                defaults: new { controller = "Products", action ="Create" });

            //Ruta de afisare a produselor de pe o pagina dintr-o categorie
            routes.MapRoute(
                name: "ProductsInCategoryByPage",
                url: "Products/{catName}/Page{page}",
                defaults: new { controller = "Products", action = "Index" });




            //Ruta de afisare a produselor de pe o pagina
            routes.MapRoute(
                name: "ProductsByPage",
                url: "Products/Page{page}",
                defaults: new { controller = "Products", action = "Index" });

            //Ruta de afisare a produselor dintr-o categorie
            routes.MapRoute(
                 name: "ProductsInCategory",
                 url: "Products/{catName}",
                 defaults: new { controller = "Products", action = "Index" });

            //Ruta de afisare a indexului produselor
            routes.MapRoute(
                name: "ProductsIndex",
                url: "Products",
                defaults: new { controller = "Products", action = "Index" });
                              
            //Ruta implicita (cea mai generala)
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

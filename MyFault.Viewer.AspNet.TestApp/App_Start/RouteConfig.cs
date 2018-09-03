using System.Runtime.Remoting.Messaging;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyFault.Viewer.AspNet.TestApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default", "{controller}/{action}", new { controller = "Test", action = "Index", id = UrlParameter.Optional });
        }
    }
}
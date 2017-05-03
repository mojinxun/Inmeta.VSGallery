using System.Linq;
using System.Web.Http;

namespace Inmeta.VSGallery.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { controller = "Download", id = RouteParameter.Optional });
            config.Routes.MapHttpRoute(name: "Extension", routeTemplate: "api/{controller}/{vsixid}");
        }
    }
}

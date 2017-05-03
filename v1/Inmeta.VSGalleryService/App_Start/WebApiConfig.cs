using System.Web.Http;

namespace Inmeta.VSGalleryService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new {controller = "Gallery", id = RouteParameter.Optional}
                );
            config.Routes.MapHttpRoute(
                name: "ExtensionCategory",
                routeTemplate: "{controller}/{action}/{category}/{name}");
            config.Routes.MapHttpRoute(
                name: "Extension",
                routeTemplate: "{controller}/{action}/{name}");
        }
    }
}

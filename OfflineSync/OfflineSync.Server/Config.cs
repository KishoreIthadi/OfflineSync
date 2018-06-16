using System.Web.Http;

namespace OfflineSync.Server
{
    public class Config
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
               name: "SyncApi",
               routeTemplate: "api/{controller}/{action}/{id}",
               defaults: new { id = RouteParameter.Optional }
           );
        }
    }
}

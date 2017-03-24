using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(SiteMonitoringService.Startup))]

namespace SiteMonitoringService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure Web API for self-host. 
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Web Api
            app.UseWebApi(config);

        }
    }
}

using System.Web.Http;
using Cool.Module.Service;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Cool.Module.Service
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();
            app.UseCors(CorsOptions.AllowAll);

            app.UseWebApi(config);
        }
    }
}
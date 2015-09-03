using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

namespace Cool.Module.Service
{
    [assembly: OwinStartup(typeof(Startup))]
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
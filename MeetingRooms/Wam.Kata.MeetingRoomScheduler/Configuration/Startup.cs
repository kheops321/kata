using System.Web.Http;
using Microsoft.Owin;
using Owin;
using Wam.Kata.MeetingRoomScheduler.Configuration;
using Wam.Kata.MeetingRoomScheduler.Middleware.Repositories;

[assembly: OwinStartup(typeof(Startup))]

namespace Wam.Kata.MeetingRoomScheduler.Configuration
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            
            ConfigureRoutes(config);
            ConfigureUnity(config);
            ConfigureLog(config);
            ConfigureFormatting(config);
            ConfigureValidation(config);
            ConfigureSwagger(config);

            Database.InitDatabase();

            app.UseWebApi(config);
        }
    }
}
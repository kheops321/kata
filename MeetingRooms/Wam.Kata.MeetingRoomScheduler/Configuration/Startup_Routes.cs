using System.Web.Http;

namespace Wam.Kata.MeetingRoomScheduler.Configuration
{
    public partial class Startup
    {
        public void ConfigureRoutes(HttpConfiguration configuration)
        {
            configuration.MapHttpAttributeRoutes();
            configuration.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
        }
    }
}
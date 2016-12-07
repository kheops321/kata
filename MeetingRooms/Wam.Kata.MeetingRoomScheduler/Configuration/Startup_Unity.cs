using System.Web.Http;
using Microsoft.Practices.Unity;
using Unity.WebApi;
using Wam.Kata.MeetingRoomScheduler.Middleware.Repositories;

namespace Wam.Kata.MeetingRoomScheduler.Configuration
{
    public partial class Startup
    {
        public void ConfigureUnity(HttpConfiguration configuration)
        {
            var container = new UnityContainer();

            container.RegisterType<IRoomRepository, RoomRepository>();
            container.RegisterType<IMeetingRepository, MeetingRepository>();

            configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
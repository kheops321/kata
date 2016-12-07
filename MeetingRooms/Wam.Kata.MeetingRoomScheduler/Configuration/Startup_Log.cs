using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Wam.Kata.MeetingRoomScheduler.Filters;
using Wam.Kata.MeetingRoomScheduler.Middleware.Log;

namespace Wam.Kata.MeetingRoomScheduler.Configuration
{
    public partial class Startup
    {
        public void ConfigureLog(HttpConfiguration configuration)
        {
            ApiLogger.Current.Enable();

            configuration.Filters.Add(new LogActionFilter());
            configuration.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
        }
    }
}
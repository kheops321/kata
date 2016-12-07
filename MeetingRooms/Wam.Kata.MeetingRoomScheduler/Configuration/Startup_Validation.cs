using System.Web.Http;
using Wam.Kata.MeetingRoomScheduler.Filters;

namespace Wam.Kata.MeetingRoomScheduler.Configuration
{
    public partial class Startup
    {
        public void ConfigureValidation(HttpConfiguration configuration)
        {
            configuration.Filters.Add(new ValidateModelAttribute());
        }
    }
}
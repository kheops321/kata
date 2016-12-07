using System.Net.Http.Formatting;
using System.Web.Http;

namespace Wam.Kata.MeetingRoomScheduler.Configuration
{
    public partial class Startup
    {
        public void ConfigureFormatting(HttpConfiguration configuration)
        {
            var jsonMediaTypeFormatter = new JsonMediaTypeFormatter();
            configuration.Formatters.Clear();
            configuration.Formatters.Add(jsonMediaTypeFormatter);
        }
    }
}
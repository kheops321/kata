using System;
using System.Web.Http;
using Swashbuckle.Application;

namespace Wam.Kata.MeetingRoomScheduler.Configuration
{
    public partial class Startup
    {
        public void ConfigureSwagger(HttpConfiguration configuration)
        {
            configuration
            .EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "Meeting room scheduler apis");
                c.IncludeXmlComments(
                    $@"{AppDomain.CurrentDomain.BaseDirectory}\bin\Wam.Kata.MeetingRoomScheduler.XML");

            })
            .EnableSwaggerUi();
        }
    }
}
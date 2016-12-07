using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using Wam.Kata.MeetingRoomScheduler.Middleware.Log;

namespace Wam.Kata.MeetingRoomScheduler.Filters
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            return Task.Run(delegate
            {
                var token = ApiLogger.Current.Critical(context.Exception);

                var errorMessage =
                    $"We are sorry, an internal server error occured, please contact your IT support team and provide them the following token >> {token}";

                var response = context.Request.CreateResponse(HttpStatusCode.InternalServerError,
                    new {Message = errorMessage});

                context.Result = new ResponseMessageResult(response);

            });
        }
    }


}
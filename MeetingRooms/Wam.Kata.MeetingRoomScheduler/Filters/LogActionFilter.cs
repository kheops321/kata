using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Newtonsoft.Json;
using Wam.Kata.MeetingRoomScheduler.Middleware.Log;

namespace Wam.Kata.MeetingRoomScheduler.Filters
{
    public class LogActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var httpMethod = actionContext.Request.Method.Method;
            var requestUri = actionContext.Request.RequestUri.LocalPath;
            var arguments = JsonConvert.SerializeObject(actionContext.ActionArguments);

            ApiLogger.Current.Info($"{httpMethod} / {requestUri} / ${arguments}");
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var httpMethod = actionExecutedContext.Request.Method.Method;
            var requestUri = actionExecutedContext.Request.RequestUri.LocalPath;
            var result = actionExecutedContext.Response.IsSuccessStatusCode ? "OK" : "KO";

            ApiLogger.Current.Info($"{httpMethod} / {requestUri} > ${result}");
        }
    }
}
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.WebApi.Exceptions;

namespace ProjectManagementSystem.WebApi.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private static readonly ActionDescriptor EmptyActionDescriptor = new ActionDescriptor();
        private static readonly RouteData EmptyRouteData = new RouteData();

        private readonly RequestDelegate _next;
        private readonly IActionResultExecutor<ObjectResult> _executor;
        private readonly ILoggerFactory _loggerFactory;

        public ErrorHandlingMiddleware(RequestDelegate next, IActionResultExecutor<ObjectResult> executor,
            ILoggerFactory loggerFactory)
        {
            _next = next;
            _executor = executor;
            _loggerFactory = loggerFactory;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exc)
            {
                var logger = _loggerFactory.CreateLogger("System");
                logger.LogCritical(exc, exc.Message);
                var actionContext = new ActionContext(context, context.GetRouteData() ?? EmptyRouteData,
                    EmptyActionDescriptor);
                var problemDetails = new ProblemDetails
                {
                    Title = ErrorCode.InternalServerError,
                    Status = (int) HttpStatusCode.InternalServerError,
#if DEBUG
                    Detail = exc.Message,
#else
                    Detail = "internal server error"
#endif
                };
                problemDetails.Extensions.Add("traceId", context.TraceIdentifier);

                await _executor.ExecuteAsync(actionContext, new ObjectResult(problemDetails)
                {
                    StatusCode = (int) HttpStatusCode.InternalServerError
                });
            }
        }
    }
}
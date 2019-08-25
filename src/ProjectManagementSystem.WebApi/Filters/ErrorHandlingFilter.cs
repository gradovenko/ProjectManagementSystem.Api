using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.WebApi.Exceptions;

namespace ProjectManagementSystem.WebApi.Filters
{
    public class ErrorHandlingFilter : IAsyncActionFilter
    {
        private readonly ILoggerFactory _loggerFactory;

        public ErrorHandlingFilter(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var execution = await next();
            if (execution.Exception != null && !execution.ExceptionHandled)
            {
                var logger = _loggerFactory.CreateLogger(context.ActionDescriptor.RouteValues["controller"]);
                logger.LogError(execution.Exception, string.Empty);

                execution.ExceptionHandled = true;

                if (execution.Exception is ApiException apiException)
                {
                    var problemDetails = new ProblemDetails
                    {
                        Title = apiException.Message,
                        Status = apiException.HttpStatusCode,
                        Detail = apiException.Description,
                    };

                    problemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);

                    if (apiException.Fields != null)
                    {
                        foreach (var apiExceptionField in apiException.Fields.Keys)
                        {
                            problemDetails.Extensions.Add(apiExceptionField, apiException.Fields[apiExceptionField]);
                        }
                    }

                    execution.Result = new ObjectResult(problemDetails)
                    {
                        StatusCode = apiException.HttpStatusCode
                    };
                    
                    return;
                }

                execution.Result = new ObjectResult(new ProblemDetails
                {
                    Type = ErrorCode.InternalServerError,
                    Status = (int) HttpStatusCode.InternalServerError,
#if DEBUG
                    Detail = execution.Exception.Message,
#else
                    Detail = "internal server error"
#endif
                })
                {
                    StatusCode = (int) HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
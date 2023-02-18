using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementSystem.Api.Extensions;

internal static class ControllerBaseExtensions
{
    public static ObjectResult StatusCode(this ControllerBase controller, HttpStatusCode statusCode, string type,
        string detail)
    {
        var problemDetails = new ProblemDetails
        {
            Type = type,
            Detail = detail,
            Status = (int)statusCode
        };

        return controller.StatusCode((int)statusCode, problemDetails);
    }
}
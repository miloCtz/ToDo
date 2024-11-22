using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDo.Domain.Shared;

namespace ToDo.Web.Api.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    protected readonly ISender _sender;

    public ApiController(ISender sender)
    {
        _sender = sender;
    }

    protected IActionResult HandleFailure(Result result) => result switch
    {
        IValidationResult validationResult => BadRequest(
            CreateProblemDetails(
                "Validation Error",
                StatusCodes.Status400BadRequest,
                result.Error,
                validationResult.Errors)),
        _ => BadRequest(
            CreateProblemDetails(
                "Bad Request",
                StatusCodes.Status400BadRequest,
                result.Error))
    };

    private static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        Error error,
        Error[]? errors = null)
    {
        return new()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };
    }
}
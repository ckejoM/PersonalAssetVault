using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
public abstract class ApiController : ControllerBase
{
    protected IActionResult HandleFailure(Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException("Cannot handle failure for a successful result.");
        }

        return BadRequest(new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Bad Request",
            Detail = result.Error.Description,
            Extensions = { { "errorCode", result.Error.Code } }
        });
    }
}

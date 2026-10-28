using ECommerce.Application.Commen;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIBaseController : ControllerBase
    {
        public static ActionResult<T> ToActionResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return new ObjectResult(result.data);
            }
            else
            {
                return ToProblem(result.Errors);
            }
        }
        protected static OkObjectResult ToProblem(IReadOnlyList<Error> errors)
        {
            var firstError = errors[0];
            var statusCode = firstError.ErrorType switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError

            };
            var problem = new ProblemDetails()
            {
                Status = statusCode,
                Title = firstError.Code,
                Detail = firstError.Description,
                Extensions = { ["error"] = errors }
            };

            return new OkObjectResult(problem) { StatusCode = statusCode };




        }

        protected string GetEmailFromToken()
            => User.FindFirstValue(ClaimTypes.Email)
            ?? throw new UnauthorizedAccessException("NO eamil calim found.");
    }
}

using KickTime.API.Contracts;
using KickTime.Application.Results;
using Microsoft.AspNetCore.Mvc;

namespace KickTime.API.Controller
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult HandleResult<T>(Result<T> result)
        {
            switch (result.Status)
            {
                case ResultStatus.Success:
                    return Ok(ApiResponse<T>.Ok(result.Value!, result.Message));

                case ResultStatus.ValidationFailure:
                    return BadRequest(ApiResponse<T>.Fail(result.Message, result.Errors));

                case ResultStatus.Unauthorized:
                    return Unauthorized(ApiResponse<T>.Fail(result.Message));

                case ResultStatus.NotFound:
                    return NotFound(ApiResponse<T>.Fail(result.Message));

                case ResultStatus.Conflict:
                    return Conflict(ApiResponse<T>.Fail(result.Message));

                default:
                    return StatusCode(
                        500,
                        ApiResponse<T>.Fail(result.Message));
            }
        }
    }
}

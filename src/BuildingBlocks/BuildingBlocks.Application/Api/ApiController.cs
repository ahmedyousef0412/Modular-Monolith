
namespace BuildingBlocks.Application.Api;

[ApiController]
public abstract class ApiController : ControllerBase
{
    protected IActionResult RequestResult<T>(Result<T> result)
    {
       
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return HandleFailure(result.Error);
    }

    protected IActionResult RequestResult(Result result)
    {
        if (result.IsSuccess)
        {
            return NoContent(); 
        }

        return HandleFailure(result.Error);
    }

    private IActionResult HandleFailure(Error error)
    {
        return error.Code switch
        {
            Error.NotFoundCode => NotFound(new { error.Code, error.Description }),
            Error.ValidationCode => BadRequest(new { error.Code, error.Description }),
            _ => BadRequest(new { error.Code, error.Description })
        };
    }
}
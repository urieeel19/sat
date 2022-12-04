using Microsoft.AspNetCore.Mvc;
using WebApplication1.Application;
using WebApplication1.Wrappers;

namespace WebApplication1.Web.Controllers
{
    public class ApiController : ControllerBase
    {
        protected readonly ILogger _logger;

        protected ApiController(ILogger<ApiController> logger)
        {
            _logger = logger;
        }

        protected ActionResult<Response<T>> GetResponseFromResult<T>(Result<T> result) where T : class
        {
            Dictionary<ResultType, Func<object, ObjectResult>> dictionary = new Dictionary<ResultType, Func<object, ObjectResult>>
            {
                {
                    ResultType.Ok,
                    (object d) => Ok(d)
                },
                {
                    ResultType.Invalid,
                    (object d) => BadRequest(d)
                },
                {
                    ResultType.NotFound,
                    (object d) => NotFound(d)
                },
                {
                    ResultType.UnexpectedError,
                    (object d) => StatusCode(500, d)
                },
                {
                    ResultType.Forbidden,
                    (object d) => StatusCode(403, d)
                }
            };
            return GetResponse(result, dictionary[result.ResultType]);
        }

        private ActionResult<Response<T>> GetResponse<T>(Result<T> result, Func<object, ObjectResult> getObjectResult) where T : class
        {
            Response<T> arg = new Response<T>
            {
                Data = result.Data,
                Result = result.ResultType.ToString(),
                Errors = result.ErrorMessages,
                Succeeded = result.IsSuccess()
            };
            return getObjectResult(arg);
        }

    }
}

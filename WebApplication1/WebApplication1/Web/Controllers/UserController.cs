using Microsoft.AspNetCore.Mvc;
using WebApplication1.Application;
using WebApplication1.Application.User.Create;
using WebApplication1.BusinessLogic.Services;

namespace WebApplication1.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService, ILogger<UserController> logger) : base(logger)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<Response<ResponseCreate>>> Create([FromBody] RequestCreate request)
        {
            var result = await _userService.CreateAsync(request);

            return GetResponseFromResult(result);
        }
    }
}

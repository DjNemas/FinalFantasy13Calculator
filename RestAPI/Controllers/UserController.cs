using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet, Authorize]
        [ProducesResponseType(typeof(GetUserResponse), StatusCodes.Status200OK, "application/json")]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized, "text/plain")]
        public async Task<IActionResult> GetUser()
        {
            var user = await _userService.GetUserAsync(CurrentUser!.Id, true);
            var response = Mapper.MapPropertys<GetUserResponse>(user!);
            response.Role = user!.Role.Role;
            return Ok(response);
        }
    }
}

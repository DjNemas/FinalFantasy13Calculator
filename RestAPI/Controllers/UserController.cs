using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ImageUtils _imageUtils;

        public UserController(IUserService userService, ImageUtils imageUtils)
        {
            _userService = userService;
            _imageUtils = imageUtils;
        }

        [HttpGet, Authorize]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK, "application/json")]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized, "text/plain")]
        public async Task<IActionResult> GetUser()
        {
            var user = await _userService.GetUserAsync(CurrentUser!.Id, true);
            var response = Mapper.MapPropertys<UserResponse>(user!);

            response.Role = user!.Role.Role;
            
            return Ok(response);
        }

        [HttpPut, Authorize]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
        {
            var user = await _userService.GetUserAsync(CurrentUser!.Id);

            var oldImage = user!.Base64AvatarImage;            
            Mapper.MapPropertys(request, user!);

            if (request.Base64AvatarImage is not null)
            {
                var image = Convert.FromBase64String(request.Base64AvatarImage);
                // Überprüfe, ob es sich um ein Bild handelt und ob es die Formate jpg, jpeg, png, gif unterstützt
                if(_imageUtils.IsAvatarImageFormat(image, out string? format))
                {
                    user!.Base64AvatarImage = request.Base64AvatarImage;
                    user!.AvatarImageMimeType = format;
                }                    
                else
                    user!.Base64AvatarImage = oldImage;
            }
            
            await _userService.UpdateUserAsync(user!);
            var response = Mapper.MapPropertys<UserResponse>(user!);
            return Ok(response);
        }

        [HttpGet("Avatar"), Authorize]
        [ProducesResponseType(typeof(GetAvatarResponse), StatusCodes.Status200OK, "application/json")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound, "text/plain")]
        public async Task<IActionResult> GetAvatar()
        {
            var user = await _userService.GetUserAsync(CurrentUser!.Id);
            if (user!.Base64AvatarImage is null)
                return NotFound("No avatar image found.");

            var response = Mapper.MapPropertys<GetAvatarResponse>(user!);

            return Ok(response);
        }
    }
}

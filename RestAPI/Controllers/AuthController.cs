using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shared.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK, "text/plain")]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict, "text/plain")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (await _userService.GetUserAsync(request.Email) is not null)
                return Conflict("User with the given email already exists.");

            await _authService.HashPassword(request.Password, out var passwordHash, out var passwordSalt);

            var username = string.Empty;
            do 
            {
                username = Generator.GenerateUniqueUsername();
            } 
            while (await _userService.UsernameExist(username));

            var user = new User
            {
                Email = request.Email,
                Username = username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = await _userService.GetUserRoleAsync(Roles.User)
            };
            await _userService.AddUserAsync(user);
            return Ok("User successfully registered.");
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK, "application/json")]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest, "text/plain")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _userService.GetUserAsync(request.Email);
            if (user is null)
                return BadRequest("Wrong Username or Password");

            if (!_authService.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
                return BadRequest("Wrong Username or Password");

            var session = _authService.GenerateSession(user);
            await _authService.AddSessionAsync(session);

            var response = Mapper.MapPropertys<LoginResponse>(session);
            return Ok(response);
        }

        [HttpPut("reset")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK, "application/json")]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized, "text/plain")]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest, "text/plain")]
        public async Task<IActionResult> Reset(ResetRequest request)
        {
            var session = await _authService.GetSessionByResetTokenAsync(request.ResetToken, true);
            if (session is null || session.User.Id != request.UserId)
                return BadRequest("Unauthorized");

            if (session.ExpirationDateResetToken.UtcDateTime <= DateTime.UtcNow)
                return Unauthorized("Token expired. Please log in again.");

            var newSession = _authService.GenerateSession(session.User);
            newSession.Id = session.Id;
            Mapper.MapPropertys(newSession, session);

            await _authService.UpdateSession(session);

            var response = Mapper.MapPropertys<LoginResponse>(session);
            return Ok(response);
        }

        [HttpDelete("logout"), Authorize]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK, "text/plain")]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized, "text/plain")]
        public async Task <IActionResult> Logout()
        {
            var session = await _authService.GetSessionByBearerTokenAsync(Request.Headers["Authorization"].First()!.Substring(7));
            await _authService.RemoveSessionAsync(session!);
            return Ok("User successfully logged out.");
        }
    }
}

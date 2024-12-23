using Homepage.Models.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Homepage.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var httpContext = Request;
            var response = await _authService.LoginAsync(username, password);

            var viewModel = new IndexViewModel();
            if (!response.Success)
            {
                viewModel.Message = response.Message;
                return RedirectToAction("Index", "Home", viewModel);
            }
            else
            {
                HttpContext.Session.SetString("miku", response.Data!.BearerToken);

                viewModel.Message = "Login successful!";
                viewModel.IsSuccessMessage = true;
                return RedirectToAction("Index", "Home", viewModel);
            }
        }

        [HttpGet("Logout"), Authorize]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("miku");

            var viewModel = new IndexViewModel();
            viewModel.Message = "Logout successful!";
            viewModel.IsSuccessMessage = true;
            return RedirectToAction("Index", "Home", viewModel);
        }
    }
}

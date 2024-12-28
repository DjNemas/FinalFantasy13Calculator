using Homepage.Models.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Homepage.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(
            IWebHostEnvironment environment,
            HttpClient httpClient,
            IAuthService authService) : base(environment, httpClient)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var response = await _authService.LoginAsync(email, password);

            if (!response.Success)
            {
                TempData["Message"] = response.Message;
                TempData["IsError"] = true;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                HttpContext.Session.SetString("miku", response.Data!.BearerToken);

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string email, string password)
        {
            var response = await _authService.RegisterAsync(email, password);
            if (!response.Success)
            {
                TempData["Message"] = response.Message;
                TempData["IsError"] = true;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Message"] = response.Data;
                TempData["IsError"] = false;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet("Logout"), Authorize]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("miku");

            var viewModel = new IndexViewModel();
            viewModel.Message = "Logout successful!";
            viewModel.IsSuccessMessage = true;
            return RedirectToAction("Index", "Home", viewModel);
        }
    }
}

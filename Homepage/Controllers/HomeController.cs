using Homepage.Extensions;
using Homepage.Models;
using Homepage.Models.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Homepage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public IActionResult Index(IndexViewModel? viewModel = null)
        {
            var test = HttpContext.Session.GetString("miku");
            var httpClient = _httpClient;
            viewModel = viewModel ?? new IndexViewModel();
            viewModel.FillNavigationContent();
            return View(viewModel);
        }

        [Authorize]
        public IActionResult Privacy()
        {
            var httpClient = _httpClient;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

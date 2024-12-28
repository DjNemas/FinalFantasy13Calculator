using Homepage.Extensions;
using Homepage.Models;
using Homepage.Models.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Homepage.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(
            IWebHostEnvironment environment,
            HttpClient httpClient,
            ILogger<HomeController> logger) : base(environment, httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public IActionResult Index(IndexViewModel? viewModel = null)
        {
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

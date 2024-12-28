using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Homepage.Models;
using System.Security.Claims;

namespace Homepage.Controllers
{
    public class BaseController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly HttpClient _httpClient;
        protected CurrentUser? _currentUser;

        public BaseController(IWebHostEnvironment environment, HttpClient httpClient)
        {
            _environment = environment;
            _httpClient = httpClient;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            SetDefaultAvatar();
            SetCurrentUser().Wait();

            base.OnActionExecuting(context);
        }

        private async Task SetCurrentUser()
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                _currentUser = new CurrentUser
                {
                    Id = uint.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value),
                    Username = HttpContext.User.FindFirst(ClaimTypes.Name)!.Value,
                    Role = (Roles)Enum.Parse(typeof(Roles), HttpContext.User.FindFirst(ClaimTypes.Role)!.Value)
                };
                await SetViewBag();
            }
        }

        private async Task SetViewBag()
        {
            ViewBag.Username = _currentUser!.Username;
            await SetUserAvatar();
        }

        private void SetDefaultAvatar()
        {
            var defaultAvatar = _environment.WebRootFileProvider.GetFileInfo("res/image/shared/default-avatar.jpg");
            var imageBytes = System.IO.File.ReadAllBytes(defaultAvatar.PhysicalPath!);
            ViewBag.AvatarImage = Convert.ToBase64String(imageBytes);
            ViewBag.AvatarMimeType = "image/jpeg";
        }

        private async Task SetUserAvatar()
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<GetAvatarResponse>("user/avatar");
                if (result is not null)
                {
                    ViewBag.AvatarImage = result.Base64AvatarImage;
                    ViewBag.AvatarMimeType = result.AvatarImageMimeType;
                }
            }
            catch { }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RestAPI.Models;
using System.Security.Claims;

namespace RestAPI.Controllers
{
    public abstract class BaseController : Controller
    {
        protected CurrentUser? CurrentUser;

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(User.Identity is not null && User.Identity.IsAuthenticated)
            {
                CurrentUser = new CurrentUser
                {
                    Id = uint.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value),
                    Role = (Roles)Enum.Parse(typeof(Roles), User.FindFirst(ClaimTypes.Role)!.Value)
                };
            }
            return base.OnActionExecutionAsync(context, next);
        }
    }
}
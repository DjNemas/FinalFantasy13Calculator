using Microsoft.AspNetCore.Mvc;
using RestAPI.Interfaces;

namespace RestAPI.Controllers
{
    public abstract class BaseController : ControllerBase
    {
         protected readonly IDatabaseService _databaseService;

        public BaseController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }
    }
}

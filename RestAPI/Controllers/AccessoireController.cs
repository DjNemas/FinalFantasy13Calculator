using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Database.Models;
using RestAPI.DTOs;
using RestAPI.Interfaces;
using RestAPI.Utils;
using System.ComponentModel;

namespace RestAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccessoireController : BaseController
    {
        public AccessoireController(IDatabaseService databaseService) : base(databaseService) { }

        [HttpGet]
        [ProducesResponseType(typeof(AccessoireResponse), 200, "application/json")]
        [ProducesResponseType(typeof(IEnumerable<AccessoireResponse>), 200, "application/json")]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest, "text/plain")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound, "text/plain")]
        public async Task<IActionResult> GetAccessoire([Description("Search for a specific ID or 0 for all")] uint id)
        {
            if(id == 0)
            {
                var accessoire = await _databaseService.GetAccessoire();
                var response = Mapper.MapPropertys<AccessoireResponse>(accessoire);
                return Ok(response);
            }
            if (id > 0)
            {
                var accessoire = await _databaseService.GetAccessoire(id);
                if (accessoire is null)
                    return NotFound("Accessoire with the given ID not found.");

                var response = Mapper.MapPropertys<AccessoireResponse>(accessoire);
                return Ok(response);
            }
            else
                return BadRequest("ID has to be a positiv number.");            
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(AccessoireResponse), StatusCodes.Status200OK, "application/json")]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest, "text/plain")]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict, "text/plain")]
        
        public async Task<IActionResult> AddAccessoire([FromBody] AddAccessoireRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
                return BadRequest("Name is required.");

            if (await _databaseService.AccessoireExist(request.Name))
                return Conflict("Accessoire with the given name already exists.");

            if(request.UpgradeToAccessoireId is not null)
            {
                if (!await _databaseService.AccessoireExist(request.UpgradeToAccessoireId.Value))
                    return BadRequest($"The specified upgrade accessory does not exist: ID {request.UpgradeToAccessoireId}");
            }

            var accessoire = Mapper.MapPropertys<Accessoire>(request);
            accessoire = await _databaseService.AddAccessoire(accessoire);

            var response = Mapper.MapPropertys<AccessoireResponse>(accessoire);
            return Ok(response);
        }

        [HttpPut]
        [Authorize]
        [ProducesResponseType(typeof(AccessoireResponse), StatusCodes.Status200OK, "application/json")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound, "text/plain")]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict, "text/plain")]
        public async Task<IActionResult> UpdateAccessoire([FromBody] UpdateAccessoireRequest request)
        {
            var accessoire = await _databaseService.GetAccessoire(request.Id);
            if (accessoire == null)
                return NotFound("Accessoire with the given ID not found.");

            if (request.Name != accessoire.Name && await _databaseService.AccessoireExist(request.Name))
                return Conflict("Accessoire with the given Name already exist.");

            if(request.UpgradeToAccessoireId is not null)
            {
                if (request.UpgradeToAccessoireId == accessoire.Id)
                    return BadRequest("The UpgradeToAccessoireId can't be the same as this item.");
                if (!await _databaseService.AccessoireExist(request.UpgradeToAccessoireId.Value))
                    return BadRequest($"The specified upgrade accessory does not exist: ID {request.UpgradeToAccessoireId}");
            }

            var remeberUpgradeTo = accessoire.UpgradeToAccessoireId is not null && request.UpgradeToAccessoireId is null ?
                accessoire.UpgradeToAccessoireId : 
                null;

            Mapper.MapPropertys(request, accessoire);

            if (remeberUpgradeTo is not null)
                accessoire.UpgradeToAccessoireId = remeberUpgradeTo;

            await _databaseService.UpdateAccessoire(accessoire);

            var response = Mapper.MapPropertys<AccessoireResponse>(accessoire);

            return Ok(response);
        }
    }
}

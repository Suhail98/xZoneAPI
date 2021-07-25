using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Accounts;
using xZoneAPI.Repositories.AccountBadges;

/// <summary>
///  A class that represents ...
/// 
///  @see OtherClasses
///  @author your_name_here
/// </summary>
namespace xZoneAPI.Controllers.AccountBadgeControllers
{
    [Route("api/v{version:apiVersion}/AccountBadge")]
    //[Route("api/[controller]")]
    [ApiController]
    public class AccountBadgeController : ControllerBase
    {
        IAccountBadgeRepo repo;
        private readonly IMapper mapper;
        public AccountBadgeController(IAccountBadgeRepo _repo, IMapper _mapper)
        {
            repo = _repo;
            mapper = _mapper;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddBadge([FromBody] AccountBadge accountBadge)
        {
            if (accountBadge == null)
            {
                return BadRequest(ModelState);
            }
            // TODO verify whether tasks exists or not            
            var OperationStatus = repo.AddAccountBadge(accountBadge);
            if (!OperationStatus)
            {
                ModelState.AddModelError("", $"Something wrong in adding Badge");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAccountBadges()
        {
            var objList = repo.GetAllAccountBadges();
            var dtoList = new List<AccountBadge>();
            foreach (var obj in objList)
            {
                dtoList.Add((AccountBadge)obj);
            }
            return Ok(dtoList);
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAccountBadgesByID(int id)
        {
            ICollection<AccountBadge> AccountBadges = repo.GetAllBadgesForAccount(id);
            if (AccountBadges == null)
                return NotFound();
            return Ok(AccountBadges);
        }
        [HttpDelete("{accountId:int}/{BadgeId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteAccountBadge(int accountId, int BadgeId)
        {
            AccountBadge AccountBadge = repo.GetAccountBadge(accountId,BadgeId);
            if (AccountBadge == null)
                return NotFound("Check Id");
            if(!repo.DeleteAccountBadge(AccountBadge))
            {
                ModelState.AddModelError("", $"something went wrong deleting the record");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        
    } 
}
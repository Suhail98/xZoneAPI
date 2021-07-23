using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Accounts;
using xZoneAPI.Repositories.AccountRepo;
using xZoneAPI.Repositories.Skills;

/// <summary>
///  A class that represents ...
/// 
///  @see OtherClasses
///  @author your_name_here
/// </summary>
namespace xZoneAPI.Controllers.AccountSkillControllers
{
    [Route("api/v{version:apiVersion}/AccountSkill")]
    //[Route("api/[controller]")]
    [ApiController]
    public class AccountSkillController : ControllerBase
    {
        IAccountSkillRepo repo;
        private readonly IMapper mapper;
        public AccountSkillController(IAccountSkillRepo _repo, IMapper _mapper)
        {
            repo = _repo;
            mapper = _mapper;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddSkill([FromBody] AccountSkill accountSkill)
        {
            if (accountSkill == null)
            {
                return BadRequest(ModelState);
            }
            // TODO verify whether tasks exists or not            
            var OperationStatus = repo.AddAccountSkill(accountSkill);
            if (!OperationStatus)
            {
                ModelState.AddModelError("", $"Something wrong in adding Skill");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAccountSkills()
        {
            var objList = repo.GetAllAccountSkills();
            var dtoList = new List<AccountSkill>();
            foreach (var obj in objList)
            {
                dtoList.Add((AccountSkill)obj);
            }
            return Ok(dtoList);
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAccountSkillsByID(int id)
        {
            ICollection<int> AccountSkills = repo.GetAccountSkillsId(id);
            if (AccountSkills == null || AccountSkills.Count() == 0)
                return NotFound();
            return Ok(AccountSkills);
        }

        [HttpDelete("{accountId:int}/{SkillId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteAccountSkill(int accountId, int SkillId)
        {
            AccountSkill AccountSkill = repo.GetAccountSkill(accountId,SkillId);
            if (AccountSkill == null)
                return NotFound("Check Id");
            if(!repo.DeleteAccountSkill(AccountSkill))
            {
                ModelState.AddModelError("", $"something went wrong deleting the record");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        
    } 
}
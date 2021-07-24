using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Accounts;
using xZoneAPI.Models.Zones;
using xZoneAPI.Repositories.AccountRepo;
using xZoneAPI.Repositories.TaskRepo;
using xZoneAPI.Repositories.ZoneRepo;

namespace xZoneAPI.Controllers.AccountControllers
{
    
    [Route("api/v{version:apiVersion}/AccountZoneTask")]
    [ApiController]
    public class AccountZoneTaskController : ControllerBase
    {
        private IAccountZoneTaskRepo repo;
        private IZoneTaskRepository ZoneTaskRepo;
        private IZoneMembersRepository ZoneMembersRepo;
        private readonly IMapper mapper;

        public AccountZoneTaskController(IAccountZoneTaskRepo repo, IMapper mapper, IZoneTaskRepository zoneTaskRepo, IZoneMembersRepository zoneMembersRepo)
        {
            this.repo = repo;
            this.mapper = mapper;
            ZoneTaskRepo = zoneTaskRepo;
            ZoneMembersRepo = zoneMembersRepo;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddAccountZoneTask([FromBody] AccountZoneTask AccountZoneTask)
        {
            if (AccountZoneTask == null)
            {
                return BadRequest(ModelState);
            }
            // TODO verify whether tasks exists or not            
            var OperationStatus = repo.AddTask(AccountZoneTask);
            if (OperationStatus == null)
            {
                ModelState.AddModelError("", $"Something wrong in adding AccountZoneTask");
                return StatusCode(500, ModelState);
            }
            return Ok(OperationStatus);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAccountZoneTasks()
        {
            var objList = repo.GetAllAccountZoneTasks();
            return Ok(objList);
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAccountZoneTasksByID(int id)
        {
            ICollection<AccountZoneTask> AccountZoneTasks = repo.GetAllAccountZoneTasksForAccount(id);
            if (AccountZoneTasks == null)
                return NotFound();
            return Ok(AccountZoneTasks);
        }
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult FinishZoneTask([FromBody] AccountZoneTask accountZoneTask)
        {          
            if (repo.GetAllAccountZoneTasks(accountZoneTask.AccountID,
                accountZoneTask.ZoneTaskID) == null)
                return NotFound();
            accountZoneTask.CompleteDate = DateTime.Now;
            int zoneId = ZoneTaskRepo.GetTask(accountZoneTask.ZoneTaskID).ZoneId;
            ZoneMember zoneMember = ZoneMembersRepo.AddCompletedTask(accountZoneTask.AccountID,zoneId);
            return Ok(zoneMember);
        }


    }
}

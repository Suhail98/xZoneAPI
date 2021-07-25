using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Logic;
using xZoneAPI.Models.TaskModel;
using xZoneAPI.Models.Zones;
using xZoneAPI.Repositories.AccountRepo;
using xZoneAPI.Repositories.TaskRepo;
using xZoneAPI.Repositories.ZoneRepo;

namespace xZoneAPI.Controllers.TaskControllers
{
    [Route("api/v{version:apiVersion}/Zonetask")]
    [ApiController]
    public class ZoneTaskController : ControllerBase
    {
        private IZoneTaskRepository ZoneTaskRepo;
        private IAccountZoneTaskRepo AccountZoneTaskRepo;
        private IZoneMembersRepository ZoneMemberRepo;
        private readonly IMapper mapper;
        private readonly IGamificationLogic gamificationLogic;
        public ZoneTaskController(IZoneTaskRepository _TaskRepo, IMapper _mapper, IGamificationLogic gamificationLogic, IZoneTaskRepository zoneTaskRepo, IAccountZoneTaskRepo accountZoneTaskRepo, IZoneMembersRepository zoneMemberRepo)
        {
            ZoneTaskRepo = _TaskRepo;
            mapper = _mapper;
            this.gamificationLogic = gamificationLogic;
            ZoneTaskRepo = zoneTaskRepo;
            AccountZoneTaskRepo = accountZoneTaskRepo;
            ZoneMemberRepo = zoneMemberRepo;
        }

        [HttpGet("{zoneId:int}")]
        public IActionResult GetZoneTasks(int zoneId)
        {
            ICollection<ZoneTask> TaskList = ZoneTaskRepo.GetTasks(zoneId);
            return Ok(TaskList);
        }

        [HttpPatch("{TaskId:int}")]
        public IActionResult EditZoneTask(int TaskId, [FromBody] TaskDto taskDto)
        {
            var xTask = mapper.Map<ZoneTask>(taskDto);
            xTask.Id = TaskId;
            var RetTask = ZoneTaskRepo.UpdateTask(xTask);
            return Ok(RetTask);
        }

        [HttpPost]
        public IActionResult AddZoneTask([FromBody] ZoneTaskDto taskDto)
        {
            if (taskDto == null)
            {
                return BadRequest(ModelState);
            }
            var xTask = mapper.Map<ZoneTask>(taskDto);
            xTask.publishDate = DateTime.Now;
            var OperationStatus = ZoneTaskRepo.AddTask(xTask);
            if (OperationStatus == null)
            {
                ModelState.AddModelError("", $"Something wrong in adding {taskDto.Name} task");
                return StatusCode(500, ModelState);
            }
            ICollection<ZoneMember> zoneMembers = ZoneMemberRepo.GetAllZoneMembers(taskDto.ZoneId);
            foreach (ZoneMember zoneMember in zoneMembers)
            {
                AccountZoneTaskRepo.AddTask(new Models.Accounts.AccountZoneTask()
                {
                    ZoneTaskID = OperationStatus.Id,
                    AccountID = zoneMember.AccountId
                });
            }
            return Ok(OperationStatus);
        }


        [HttpDelete("{ZoneId:int}/{TaskId:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteZoneTask(int ZoneId, int TaskId)
        {
            var xTask = ZoneTaskRepo.GetTask(TaskId);
            if (xTask == null)
            {
                return NotFound();
            }
            var OperationStatus = ZoneTaskRepo.DeleteTask(xTask);
            if (!OperationStatus)
            {
                ModelState.AddModelError("", $"Something wrong in deleting {xTask.Name} task");
                return StatusCode(500, ModelState);
            }
            ICollection<ZoneMember> zoneMembers = ZoneMemberRepo.GetAllZoneMembers(ZoneId);
            foreach (ZoneMember zoneMember in zoneMembers)
            {

                AccountZoneTaskRepo.DeleteTask(new Models.Accounts.AccountZoneTask()
                {
                    ZoneTaskID = TaskId,
                    AccountID = zoneMember.AccountId
                });
            }
            return NoContent();
        }
        /*
        [HttpPost("{taskId:int}/{userId:int}")]
        public IActionResult FinishZoneTask(int taskId, int userId)
        {
            var xTask = ZoneTaskRepo.GetTask(taskId);
            if (xTask == null)
            {
                return NotFound();
            }
            xTask = DateTime.Now;
            var OperationStatus = ZoneTaskRepo.UpdateTask(xTask);
            if (!OperationStatus)
            {
                ModelState.AddModelError("", $"Something wrong in updating {xTask.Name} task");
                return StatusCode(500, ModelState);
            }
            AchievmentsNotifications notifications = gamificationLogic.checkForNewAchievements(userId);
            return Ok(notifications);
        }

        */
        

    }
}
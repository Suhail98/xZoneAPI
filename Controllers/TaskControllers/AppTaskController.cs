using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Logic;
using xZoneAPI.Models.TaskModel;
using xZoneAPI.Repositories.TaskRepo;

namespace xZoneAPI.Controllers.TaskControllers
{
    [Route("api/v{version:apiVersion}/task")]
    [ApiController]
    public class AppTaskController : ControllerBase
    {
        private ITaskRepository TaskRepo;
        private readonly IMapper mapper;
        private readonly IGamificationLogic gamificationLogic;
        public AppTaskController(ITaskRepository _TaskRepo, IMapper _mapper, IGamificationLogic gamificationLogic)
        {
            TaskRepo = _TaskRepo;
            mapper = _mapper;
            this.gamificationLogic = gamificationLogic;
        }

        [HttpGet] 
        public IActionResult GetAppTasks(int userId)
        {
            ICollection<AppTask> TaskList = TaskRepo.GetTasks(userId);
            var RetTasks = new List<TaskDto>();
            foreach ( var task in TaskList)
            {
                RetTasks.Add(mapper.Map<TaskDto>(task));
            }
            return Ok(RetTasks);
        }

        [HttpPatch("{TaskId:int}")]
        public IActionResult EditAppTask(int TaskId, [FromBody] TaskDto taskDto)
        {
            var xTask = mapper.Map<AppTask>(taskDto);
            xTask.Id = TaskId;
            var RetTask = TaskRepo.UpdateTask( xTask );
            return Ok(RetTask);
        }

        [HttpPost]
        public IActionResult AddAppTask([FromBody] TaskDto taskDto)
        {
            if (taskDto == null)
            {
                return BadRequest(ModelState);
            }
            var xTask = mapper.Map<AppTask>(taskDto);
            var checkExisting = TaskRepo.IsTaskExists(xTask);
            if (checkExisting)
            {
                ModelState.AddModelError("", "You have task with same id");
                return BadRequest(ModelState);
            }
            var OperationStatus = TaskRepo.AddTask(xTask);
            if (!OperationStatus)
            {
                ModelState.AddModelError("", $"Something wrong in adding {taskDto.Name} task");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }
        [HttpDelete("{TaskId:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteAppTask(int TaskId)
        {
            var xTask = TaskRepo.GetTask(TaskId);
            if (!TaskRepo.IsTaskExists(xTask))
            {
                return NotFound();
            }
            var OperationStatus = TaskRepo.DeleteTask(xTask);
            if ( !OperationStatus )
            {
                ModelState.AddModelError("", $"Something wrong in deleting {xTask.Name} task");
                return StatusCode(500, ModelState);
            }
            
            return NoContent();
        }
        [HttpPost("{taskId:int}/{userId:int}")]
        public IActionResult FinishAppTask(int taskId, int userId)
        {
            var xTask = TaskRepo.GetTask(taskId);
            if (xTask == null)
            {
                return NotFound();
            }
            xTask.CompleteDate = DateTime.Now;
            var OperationStatus = TaskRepo.UpdateTask(xTask);
            if (!OperationStatus)
            {
                ModelState.AddModelError("", $"Something wrong in updating {xTask.Name} task");
                return StatusCode(500, ModelState);
            }
            AchievmentsNotifications notifications = gamificationLogic.checkForNewAchievements(userId);
            return Ok(notifications);
        }
        


    }
}

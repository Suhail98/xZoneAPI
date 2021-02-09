using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.TaskModel;
using xZoneAPI.Repositories.TaskRepo;

namespace xZoneAPI.Controllers.TaskControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppTaskController : Controller
    {
        private ITaskRepository TaskRepo;
        private readonly IMapper mapper;

        public AppTaskController(ITaskRepository _TaskRepo, IMapper _mapper)
        {
            TaskRepo = _TaskRepo;
            mapper = _mapper;
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

        [HttpGet("{TaskId:int}")]
        public IActionResult EditAppTask(int TaskId, TaskDto taskDto)
        {
            var RetTask = TaskRepo.UpdateTask(TaskId, taskDto);
            return Ok(RetTask);
        }

        [HttpPost]
        public IActionResult AddAppTask([FromBody]TaskDto taskDto)
        {
            if( taskDto == null)
            {
                return BadRequest(ModelState);
            }
            var checkExisting = TaskRepo.IsTaskExists(taskDto.Name);
            if (checkExisting )
            {
                ModelState.AddModelError("", "You have task with same name");
            }
            if ( !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var OperationStatus = TaskRepo.AddTask(taskDto);
            if (OperationStatus == null)
            {
                ModelState.AddModelError("", $"Something wrong in adding {taskDto.Name} task");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }

        [HttpGet("{TaskId:int}")]
        public IActionResult DeleteAppTask(int TaskId)
        {
            if (!TaskRepo.IsTaskExists(TaskId))
            {
                return NotFound();
            }
            var OperationStatus = TaskRepo.DeleteTask(TaskId);
            if ( !OperationStatus )
            {
                ModelState.AddModelError("", $"Something wrong in deleting {TaskRepo.GetTask(TaskId).Name} task");
                return StatusCode(500, ModelState);
            }
            
            return NoContent();
        }




    }
}

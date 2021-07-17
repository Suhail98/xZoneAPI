using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.ProjectTaskModel;
using xZoneAPI.Models.TaskModel;
using xZoneAPI.Repositories.TaskRepo;

namespace xZoneAPI.Controllers.TaskControllers
{
    [Route("api/v{version:apiVersion}/ProjectTask")]
    [ApiController]
    public class ProjectTaskController : ControllerBase
    {
        private IProjectTaskRepository TaskRepo;
        private readonly IMapper mapper;

        public ProjectTaskController(IProjectTaskRepository _TaskRepo, IMapper _mapper)
        {
            TaskRepo = _TaskRepo;
            mapper = _mapper;
        }

        [HttpGet]
        public IActionResult GetProjectTasks(int sectionId)
        {
            ICollection<ProjectTask> TaskList = TaskRepo.GetTasks(sectionId);
            /*var RetTasks = new List<ProjectTask>();
            foreach (var task in TaskList)
            {
                RetTasks.Add(mapper.Map<TaskDto>(task));
            }*/
            return Ok(TaskList);
        }

        [HttpPatch("{TaskId:int}")]
        public IActionResult EditProjectTask(int TaskId, [FromBody] ProjectTaskDto taskDto)
        {
            var xTask = mapper.Map<ProjectTask>(taskDto);
            xTask.Id = TaskId;
            var RetTask = TaskRepo.UpdateTask(xTask);
            return Ok(RetTask);
        }

        [HttpPost]
        public IActionResult AddProjectTask([FromBody] ProjectTaskDto taskDto)
        {
            if (taskDto == null)
            {
                return BadRequest(ModelState);
            }
            var xTask = mapper.Map<ProjectTask>(taskDto);
            var checkExisting = TaskRepo.IsTaskExists(xTask);
            if (checkExisting)
            {
                ModelState.AddModelError("", "You have task with same id");
                return BadRequest(ModelState);
            }
            var OperationStatus = TaskRepo.AddTask(xTask);
            if (OperationStatus == null)
            {
                ModelState.AddModelError("", $"Something wrong in adding {taskDto.Name} task");
                return StatusCode(500, ModelState);
            }
            return Ok(OperationStatus);
        }

        [HttpDelete("{TaskId:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteProjectTask(int TaskId)
        {
            var xTask = TaskRepo.GetTask(TaskId);
            if (!TaskRepo.IsTaskExists(xTask))
            {
                return NotFound();
            }
            var OperationStatus = TaskRepo.DeleteTask(xTask);
            if (!OperationStatus)
            {
                ModelState.AddModelError("", $"Something wrong in deleting {xTask.Name} task");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }




    }
}

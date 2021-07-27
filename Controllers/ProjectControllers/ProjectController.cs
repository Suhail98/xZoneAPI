using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using xZoneAPI.Models.ProjectModel;
using xZoneAPI.Models.RoadmapModel;
using xZoneAPI.Repositories.ProjectRepo;

namespace xZoneAPI.Controllers.ProjectControllers
{
    [Route("api/v{version:apiVersion}/project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProjectRepository ProjectRepo;
        private readonly IMapper mapper;

        public ProjectController(IProjectRepository _ProjectRepo, IMapper _mapper)
        {
            ProjectRepo = _ProjectRepo;
            mapper = _mapper;
        }

        [HttpGet]
        public IActionResult GetProjects()
        {
            ICollection<Project> ProjectList = ProjectRepo.GetAllProjects();
            var RetProjects = new List<Project>();
            foreach (var Project in ProjectList)
            {
                RetProjects.Add(mapper.Map<Project>(Project));
            }
            return Ok(RetProjects);
        }

        [HttpPatch("{ProjectId:int}")]
        public IActionResult EditProject(int ProjectId, [FromBody]ProjectDto Project)
        {
            if (Project == null)
            {
                return BadRequest(ModelState);
            }
         //   var baseProject = ProjectRepo.FindProjectById(ProjectId);
            var xProject = mapper.Map<Project>(Project);
            xProject.Id = ProjectId;
            var RetProject = ProjectRepo.UpdateProject(xProject);
            return Ok(RetProject);
        }

        [HttpPost]
        public IActionResult AddProject([FromBody]ProjectDto Project)
        {
            if (Project == null)
            {
                return BadRequest(ModelState);
            }
            var xProject = mapper.Map<Project>(Project);
            var OperationStatus = ProjectRepo.addProject(xProject);
            if (OperationStatus == null)
            {
                ModelState.AddModelError("", $"Something wrong in adding {Project.Name} Project");
                return StatusCode(500, ModelState);
            }
            return Ok(xProject);
        }
        [HttpPost("roadmap")]
        public IActionResult AddProjectFromRoadmap(RoadmapRequesterDto roadmap)
        {
            var OperationStatus = ProjectRepo.addProject(roadmap.roadmapId, roadmap.RequesterId);
            return Ok(OperationStatus);
        }
        [HttpDelete("{ProjectId:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteProject(int ProjectId)
        {
            var xProject = ProjectRepo.FindProjectById(ProjectId);
            if (xProject == null)
            {
                return NotFound();
            }
            var OperationStatus = ProjectRepo.DeleteProject(xProject);
            if (!OperationStatus)
            {
                ModelState.AddModelError("", $"Something wrong in deleting {xProject.Name} Project");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }




    }
}

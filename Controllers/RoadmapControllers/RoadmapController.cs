using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.RoadmapModel;
using xZoneAPI.Repositories.RoadmapRepo;

namespace xZoneAPI.Controllers.RoadmapControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoadmapController : ControllerBase
    {
        private IRoadmapRepository RoadmapRepo;
        private readonly IMapper mapper;

        public RoadmapController(IRoadmapRepository _RoadmapRepo, IMapper _mapper)
        {
            RoadmapRepo = _RoadmapRepo;
            mapper = _mapper;
        }

        [HttpGet]
        public IActionResult GetRoadmaps()
        {
            ICollection<Roadmap> RoadmapList = RoadmapRepo.GetAllRoadmaps();
            var RetRoadmaps = new List<Roadmap>();
            foreach (var Roadmap in RoadmapList)
            {
                RetRoadmaps.Add(mapper.Map<Roadmap>(Roadmap));
            }
            return Ok(RetRoadmaps);
        }

        [HttpGet("{RoadmapId:int}")]
        public IActionResult GetRoadmap(int RoadmapId)
        {
            Roadmap roadmap = RoadmapRepo.FindRoadmapById(RoadmapId);
            return Ok(roadmap);
        }

        [HttpPatch("{RoadmapId:int}")]
        public IActionResult EditRoadmap(int RoadmapId, [FromBody] RoadmapDto Roadmap)
        {
            if (Roadmap == null)
            {
                return BadRequest(ModelState);
            }
            //   var baseRoadmap = RoadmapRepo.FindRoadmapById(RoadmapId);
            var xRoadmap = mapper.Map<Roadmap>(Roadmap);
            xRoadmap.Id = RoadmapId;
            var RetRoadmap = RoadmapRepo.UpdateRoadmap(xRoadmap);
            return Ok(RetRoadmap);
        }

        [HttpPost]
        public IActionResult AddRoadmap([FromBody] RoadmapDto Roadmap)
        {
            if (Roadmap == null)
            {
                return BadRequest(ModelState);
            }
            var xRoadmap = mapper.Map<Roadmap>(Roadmap);
            var OperationStatus = RoadmapRepo.addRoadmap(xRoadmap);
            if (OperationStatus == null)
            {
                ModelState.AddModelError("", $"Something wrong in adding {Roadmap.Name} Roadmap");
                return StatusCode(500, ModelState);
            }
            return Ok(OperationStatus);
        }

        [HttpDelete("{RoadmapId:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteRoadmap(int RoadmapId)
        {
            var xRoadmap = RoadmapRepo.FindRoadmapById(RoadmapId);
            if (xRoadmap == null)
            {
                return NotFound();
            }
            var OperationStatus = RoadmapRepo.DeleteRoadmap(xRoadmap);
            if (!OperationStatus)
            {
                ModelState.AddModelError("", $"Something wrong in deleting {xRoadmap.Name} Roadmap");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetSkillByName(string name)
        {
            List<Roadmap> Roadmaps = RoadmapRepo.FindRoadmapsByName(name);
            if (Roadmaps == null || Roadmaps.Count() == 0)
                return NotFound();
            return Ok(Roadmaps);
        }


    }
}

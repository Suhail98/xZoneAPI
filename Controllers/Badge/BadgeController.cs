using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using xZoneAPI.Models.Badges;
using xZoneAPI.Repositories.Badges;

namespace xZoneAPI.Controllers.Badges
{
    
    [Route("api/v{version:apiVersion}/Badge")]
    //[Route("api/[controller]")]
    [ApiController]
    public class BadgeController : ControllerBase
    {
        IBadgeRepo repo;
        private readonly IMapper mapper;
        public BadgeController(IBadgeRepo _repo, IMapper _mapper)
        {
            repo = _repo;
            mapper = _mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddBadge([FromBody] Badge Badge)
        {
            if (Badge == null)
            {
                return BadRequest(ModelState);
            }
            // TODO verify whether tasks exists or not            
            var OperationStatus = repo.AddBadge(Badge);
            if (OperationStatus == null)
            {
                ModelState.AddModelError("", $"Something wrong in adding {Badge.Name} Badge");
                return StatusCode(500, ModelState);
            }
            return Ok(OperationStatus);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetBadges()
        {
            var objList = repo.GetAllBadges();
            var dtoList = new List<Badge>();
            foreach (var obj in objList)
            {
                dtoList.Add((Badge)obj);
            }
            return Ok(dtoList);
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetBadgeByID(int id)
        {
            Badge Badge = repo.FindBadgeById(id);
            if (Badge == null)
                return NotFound();
            return Ok(Badge);
        }
        
        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetBadgeByName(string name)
        {
            Badge Badge = repo.FindBadgeByName(name);
            if (Badge == null)
                return NotFound();
            return Ok(Badge);
        }
        
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteBadge(int id)
        {
            Badge Badge = repo.FindBadgeById(id);
            if (Badge == null)
                return NotFound("Check Id");
            if (!repo.DeleteBadge(Badge))
            {
                ModelState.AddModelError("", $"something went wrong deleting the record{Badge.Id}");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateBadge(int id, [FromBody] Badge Badge)
        {
            if (Badge == null)
            {
                return BadRequest(ModelState);
            }
            Badge NewBadge = mapper.Map<Badge>(Badge);
            NewBadge.Id = id;
            if (!repo.UpdateBadge(NewBadge))
            {
                ModelState.AddModelError("", $"something went wrong updating the record{Badge.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
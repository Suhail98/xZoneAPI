using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using xZoneAPI.Models.Ranks;
using xZoneAPI.Repositories.Ranks;

namespace xZoneAPI.Controllers.Ranks
{
    [Route("api/v{version:apiVersion}/Rank")]
    //[Route("api/[controller]")]
    [ApiController]
    public class RankController : ControllerBase
    {
        IRankRepo repo;
        private readonly IMapper mapper;
        public RankController(IRankRepo _repo, IMapper _mapper)
        {
            repo = _repo;
            mapper = _mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddRank([FromBody] Rank Rank)
        {
            if (Rank == null)
            {
                return BadRequest(ModelState);
            }
            // TODO verify whether tasks exists or not            
            var OperationStatus = repo.AddRank(Rank);
            if (!OperationStatus)
            {
                ModelState.AddModelError("", $"Something wrong in adding {Rank.Name} Rank");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetRanks()
        {
            var objList = repo.GetAllRanks();
            var dtoList = new List<Rank>();
            foreach (var obj in objList)
            {
                dtoList.Add((Rank)obj);
            }
            return Ok(dtoList);
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetRankByID(int id)
        {
            Rank Rank = repo.FindRankById(id);
            if (Rank == null)
                return NotFound();
            return Ok(Rank);
        }
        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetRankByName(string name)
        {
            Rank Rank = repo.FindRankByName(name);
            if (Rank == null)
                return NotFound();
            return Ok(Rank);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteRank(int id)
        {
            Rank Rank = repo.FindRankById(id);
            if (Rank == null)
                return NotFound("Check Id");
            if (!repo.DeleteRank(Rank))
            {
                ModelState.AddModelError("", $"something went wrong deleting the record{Rank.Id}");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateRank(int id, [FromBody] Rank Rank)
        {
            if (Rank == null)
            {
                return BadRequest(ModelState);
            }
            Rank NewRank = mapper.Map<Rank>(Rank);
            NewRank.Id = id;
            if (!repo.UpdateRank(NewRank))
            {
                ModelState.AddModelError("", $"something went wrong updating the record{Rank.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
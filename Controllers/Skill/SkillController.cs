using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using xZoneAPI.Models.Skills;
using xZoneAPI.Repositories.AccountRepo;
using xZoneAPI.Repositories.Skills;

namespace xZoneAPI.Controllers.Skills
{
    
    [Route("api/v{version:apiVersion}/Skill")]
    //[Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        ISkillRepo repo;
        private readonly IMapper mapper;
        public SkillController(ISkillRepo _repo, IMapper _mapper)
        {
            repo = _repo;
            mapper = _mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddSkill([FromBody] SkillDto Skill)
        {
            if (Skill == null)
            {
                return BadRequest(ModelState);
            }
            Skill xSkill = mapper.Map<Skill>(Skill);
            // TODO verify whether tasks exists or not            
            var OperationStatus = repo.AddSkill(xSkill);
            if (OperationStatus == null)
            {
                ModelState.AddModelError("", $"Something wrong in adding {Skill.Name} Skill");
                return StatusCode(500, ModelState);
            }
            return Ok(xSkill);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetSkills()
        {
            var objList = repo.GetAllSkills();
            var dtoList = new List<Skill>();
            foreach (var obj in objList)
            {
                dtoList.Add((Skill)obj);
            }
            return Ok(dtoList);
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetSkillByID(int id)
        {
            Skill Skill = repo.FindSkillById(id);
            if (Skill == null)
                return NotFound();
            return Ok(Skill);
        }
        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetSkillByName(string name)
        {
            List<Skill> Skill = repo.FindSkillByName(name);
            if (Skill == null)
                return NotFound();
            return Ok(Skill);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteSkill(int id)
        {
            Skill Skill = repo.FindSkillById(id);
            if (Skill == null)
                return NotFound("Check Id");
            if (!repo.DeleteSkill(Skill))
            {
                ModelState.AddModelError("", $"something went wrong deleting the record{Skill.Id}");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateSkill(int id, [FromBody] SkillDto Skill)
        {
            if (Skill == null)
            {
                return BadRequest(ModelState);
            }
            Skill NewSkill = mapper.Map<Skill>(Skill);
            NewSkill.Id = id;
            if (!repo.UpdateSkill(NewSkill))
            {
                ModelState.AddModelError("", $"something went wrong updating the record{Skill.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Skills;
using xZoneAPI.Models.Zones;
using xZoneAPI.Repositories.ZoneRepo;

namespace xZoneAPI.Controllers.ZonesControllers
{
    [Route("api/v{version:apiVersion}/ZoneSkill")]
    [ApiController]
    public class ZoneSkillController : ControllerBase
    {

        private IZoneSkillRepository ZoneSkillRepo;
        private readonly IMapper mapper;

        public ZoneSkillController(IZoneSkillRepository zoneSkillRepository, IMapper _mapper)
        {
            ZoneSkillRepo = zoneSkillRepository;
            mapper = _mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddSkill([FromBody] ZoneSkill zoneSkill)
        {
            if (zoneSkill == null)
            {
                return BadRequest(ModelState);
            }
            // TODO verify whether tasks exists or not            
            var OperationStatus = ZoneSkillRepo.AddZoneSkill(zoneSkill);
            if (!OperationStatus)
            {
                ModelState.AddModelError("", $"Something wrong in adding Skill");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetZonesSkills()
        {
            var zonesSkills = ZoneSkillRepo.GetAllZoneSkills();
            return Ok(zonesSkills);
        }

        [HttpGet("{ZoneId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetZoneSkillsByZoneID(int ZoneId)
        {
            ICollection<Skill> ZoneSkills = ZoneSkillRepo.GetAllSkillsForZone(ZoneId);
            if (ZoneSkills == null)
                return NotFound();
           
            
            return Ok(ZoneSkills);
        }

        [HttpDelete("{ZoneId:int}/{SkillId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteZoneSkill(int ZoneId, int SkillId)
        {
            ZoneSkill zoneSkill = ZoneSkillRepo.GetZoneSkill(ZoneId,SkillId); 
            if (zoneSkill == null)
                return NotFound("Check Ids");
            var OperationStatus = ZoneSkillRepo.DeleteZoneSkill(zoneSkill);
            if (!OperationStatus)
            {
                ModelState.AddModelError("", $"something went wrong deleting skill");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }










    }
}

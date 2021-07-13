using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Zones;
using xZoneAPI.Repositories.ZoneRepo;

namespace xZoneAPI.Controllers.ZonesControllers
{
    [Route("api/v{version:apiVersion=1}/zone")]
    [ApiController]
    public class ZoneController : ControllerBase
    {
        private IZoneRepository ZoneRepo;
        private readonly IMapper mapper;

        public ZoneController(IZoneRepository zoneRepository, IMapper _mapper)
        {
            ZoneRepo = zoneRepository;
            mapper = _mapper;
        }

        [HttpGet]
        public IActionResult GetAllZones()
        {
            ICollection<Zone> ZonesList = ZoneRepo.GetAllZones();
            return Ok(ZonesList);
        }

        [HttpGet("details")]
        public IActionResult GetZone(int ZoneId)
        {
            Zone xZone = ZoneRepo.FindZoneById(ZoneId);
            return Ok(xZone);
        }

        [HttpPost]
        public IActionResult AddZone([FromBody] ZoneDto zoneDto)
        {
            if ( zoneDto == null)
            {
                return BadRequest(ModelState);
            }
            var zone = mapper.Map<Zone>(zoneDto);
            var status = ZoneRepo.AddZone(zone);
            if ( !status)
            {
                ModelState.AddModelError("", $"Something wrong in adding {zone.Name} Project");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }

        [HttpDelete("details")]
        public IActionResult DeleteZone(int ZoneId)
        {
            var zone = ZoneRepo.FindZoneById(ZoneId);
            if ( zone == null)
            {
                return NotFound();
            }
            var OperationStaus = ZoneRepo.DeleteZone(zone);
            if (!OperationStaus)
            {
                ModelState.AddModelError("", $"Something wrong in deleting {zone.Name} Project");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpPatch("details")]
        public IActionResult UpdateProject(int ZoneId,[FromBody] ZoneDto zoneDto)
        {
            if ( zoneDto == null )
            {
                return BadRequest(ModelState);
            }
            var zone = mapper.Map<Zone>(zoneDto);
            zone.Id = ZoneId;
            var updatedZone = ZoneRepo.UpdateZone(zone);
            return Ok(updatedZone);
        }



    }
}

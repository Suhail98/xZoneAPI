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
    [Route("api/v{version:apiVersion}/ZoneMember")]
    [ApiController]
    public class ZoneMemberController : ControllerBase
    {
        private IZoneMembersRepository ZoneMemberRepo;
        private IZoneRepository ZoneRepo;
        private readonly IMapper mapper;

        public ZoneMemberController(IZoneMembersRepository zoneMemberRepository, IZoneRepository zoneRepository, IMapper _mapper)
        {
            ZoneRepo = zoneRepository;
            ZoneMemberRepo = zoneMemberRepository;
            mapper = _mapper;
        }
        [HttpPost("{joiningCode}")]
        public IActionResult JoinZone([FromBody]ZoneMember zoneMember, string joiningCode = "")
        {
            int zoneId = zoneMember.ZoneId;
            Zone zone = ZoneRepo.FindZoneById(zoneId);

            if (zone == null)
            {
                return BadRequest(ModelState);
            }
            if (zone.JoinCode != joiningCode)
            {
                return BadRequest(ModelState);
            }
            var OperationStaus = ZoneMemberRepo.AddZoneMember(zoneMember);
            if (!OperationStaus)
            {
                ModelState.AddModelError("", $"Something wrong in joining {zone.Name} Project");
                return StatusCode(500, ModelState);
            }
            return Ok();
        } 

        [HttpDelete]
        public IActionResult LeaveZone([FromBody]ZoneMember zoneMember)
        {
            if (zoneMember == null)
            {
                return NotFound();
            }
            Zone zone = ZoneRepo.FindZoneById(zoneMember.ZoneId);
            var OperationStaus = ZoneMemberRepo.RemoveZoneMember(zoneMember);
            if (!OperationStaus)
            {
                ModelState.AddModelError("", $"Something wrong in leaving {zone.Name} Project");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpPatch]
        public IActionResult UpdateZoneMember([FromBody]ZoneMember zoneMember)
        {
            if ( zoneMember == null)
            {
                return BadRequest(ModelState);
            }
            var operationStatus = ZoneMemberRepo.UpdateZoneMember(zoneMember);
            if ( !operationStatus)
            {
                ModelState.AddModelError("", $"Something wrong in updating information Project");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }



    }
}

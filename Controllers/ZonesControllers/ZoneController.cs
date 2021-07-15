using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Zones;
using xZoneAPI.Repositories.AccountRepo;
using xZoneAPI.Repositories.ZoneRepo;

namespace xZoneAPI.Controllers.ZonesControllers
{
    [Route("api/v{version:apiVersion}/Zone")]
    [ApiController]
    public class ZoneController : ControllerBase
    {
        private IZoneRepository ZoneRepo;
        private IAccountRepo AccountRepo;
        private IZoneMembersRepository ZoneMemberRepo;
        private readonly IMapper mapper;

        public ZoneController(IZoneRepository zoneRepository, IAccountRepo accountRepo,IMapper _mapper)
        {
            ZoneRepo = zoneRepository;
            AccountRepo = accountRepo;
            mapper = _mapper;
        }

        [HttpGet]
        public IActionResult GetAllZones()
        {
            ICollection<Zone> ZonesList = ZoneRepo.GetAllZones();
            if ( ZonesList.Count == 0)
            {
                return NoContent();
            }
            return Ok(ZonesList);
        }

        [HttpGet("GetZone/{ZoneId:int}")]
        public IActionResult GetZone(int ZoneId)
        {
            Zone xZone = ZoneRepo.FindZoneById(ZoneId);
            return Ok(xZone);
        }

        [HttpPost("createzone/{CreatorId:int}")]
        public IActionResult AddZone([FromBody] ZoneDto zoneDto, int CreatorId)
        {
            var CreatorStatus = AccountRepo.FindAccountById(CreatorId) != null;
            if ( !CreatorStatus )
            {
                return BadRequest(ModelState);
            }
            if ( zoneDto == null )
            {
                return BadRequest(ModelState);
            }
            
            var zone = mapper.Map<Zone>(zoneDto);
            ZoneMember Admin = new ZoneMember(zone.Id, CreatorId, ZoneMember.Roles.Admin);
            ZoneMemberRepo.AddZoneMember(Admin);

            var status = ZoneRepo.AddZone(zone);
            if ( !status)
            {
                ModelState.AddModelError("", $"Something wrong in adding {zone.Name} Project");
                return StatusCode(500, ModelState);
            }
            return Ok(zone);
        }

        [HttpDelete("DeleteZone/{ZoneId:int}/{AccountId:int}")]
        public IActionResult DeleteZone(int ZoneId, int AccountId)
        {
            var zone = ZoneRepo.FindZoneById(ZoneId);
            var zoneAdmin = ZoneMemberRepo.GetZoneMember(AccountId);
            if ( !ZoneMemberRepo.IsAdmin(zoneAdmin))
            {
                return BadRequest(ModelState);
            }
            if ( zone == null)
            {
                return NotFound();
            }

            var ZoneMembers = ZoneMemberRepo.GetAllZoneMembers(ZoneId);
            foreach ( ZoneMember member in ZoneMembers )
            {
                ZoneMemberRepo.RemoveZoneMember(member);
            }
            var OperationStaus = ZoneRepo.DeleteZone(zone);
            if (!OperationStaus)
            {
                ModelState.AddModelError("", $"Something wrong in deleting {zone.Name} Project");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpPatch("UpdateProject/{ZoneId:int}")]
        public IActionResult UpdateZone(int ZoneId,[FromBody] ZoneDto zoneDto)
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

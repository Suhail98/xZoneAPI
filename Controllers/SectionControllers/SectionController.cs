using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.SectionModel;
using xZoneAPI.Repositories.SectionRepo;

// For more information on enabling Web API for empty Sections, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace xZoneAPI.Controllers.SectionControllers
{
    [Route("api/v{version:apiVersion}/Section")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private ISectionRepository SectionRepo;
        private readonly IMapper mapper;

        public SectionController(ISectionRepository _SectionRepo, IMapper _mapper)
        {
            SectionRepo = _SectionRepo;
            mapper = _mapper;
        }

        [HttpGet]
        public IActionResult GetSections()
        {
            ICollection<Section> SectionList = SectionRepo.GetAllSections();
            var RetSections = new List<Section>();
            foreach (var Section in SectionList)
            {
                RetSections.Add(mapper.Map<Section>(Section));
            }
            return Ok(RetSections);
        }

        [HttpPatch("{SectionId:int}")]
        public IActionResult EditSection(int SectionId, [FromBody]SectionDto section)
        {
            //Section xSection = new Section(SectionId, newSectionName);
            if (section == null)
            {
                return BadRequest(ModelState);
            }
            
            var xSection = mapper.Map<Section>(section);
            xSection.Id = SectionId;
            var RetSection = SectionRepo.UpdateSection(xSection);
            return Ok(RetSection);
        }

        [HttpPost]
        public IActionResult AddSection([FromBody]SectionDto section)
        {
            //Section xSection = new Section( name);
            if (section == null)
            {
                return BadRequest(ModelState);
            }
            var xSection = mapper.Map<Section>(section);
            var OperationStatus = SectionRepo.addSection(xSection);
            if (OperationStatus == null)
            {
                ModelState.AddModelError("", $"Something wrong in adding {xSection.Name} Section");
                return StatusCode(500, ModelState);
            }
            return Ok(OperationStatus);
        }

        [HttpDelete("{SectionId:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteSection(int SectionId)
        {
            var xSection = SectionRepo.FindSectionById(SectionId);
            if (xSection == null)
            {
                return NotFound();
            }
            var OperationStatus = SectionRepo.DeleteSection(xSection);
            if (!OperationStatus)
            {
                ModelState.AddModelError("", $"Something wrong in deleting {xSection.Name} Section");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }




    }
}
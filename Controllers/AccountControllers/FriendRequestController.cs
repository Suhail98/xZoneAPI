using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Accounts;
using xZoneAPI.Repositories.AccountRepo;


namespace xZoneAPI.Controllers.AccountControllers
{
    [Route("api/v{version:apiVersion}/FriendRequest")]
    //[Route("api/[controller]")]
    [ApiController]
    public class FriendRequestController : ControllerBase
    {
        IFriendRequestRepository repo;
        private readonly IMapper mapper;
        public FriendRequestController(IFriendRequestRepository _repo, IMapper _mapper)
        {
            repo = _repo;
            mapper = _mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddFriendRequest([FromBody] FriendRequest FriendRequest)
        {
            if (FriendRequest== null)
            {
                return BadRequest(ModelState);
            }
            // TODO verify whether tasks exists or not            
            var OperationStatus = repo.AddFriendRequest(FriendRequest);
            if (OperationStatus == null)
            {
                ModelState.AddModelError("", $"Something wrong in adding FriendRequest");
                return StatusCode(500, ModelState);
            }
            return Ok(OperationStatus);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetFriendRequests()
        {
            ICollection<FriendRequest> objList = repo.GetAllFriendRequests();
            return Ok(objList);
        }
        /// <summary>
        /// get friend requests which the user sent
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IDs</returns>
        [HttpGet("Sent/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetSentFriendRequestByID(int id)
        {
            ICollection<int> FriendRequest = repo.GetAllSentFriendRequestsForAccount(id);
            if (FriendRequest == null)
                return NotFound();
            return Ok(FriendRequest);
        }
        /// <summary>
        /// get friend requests which the user received
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IDs</returns>
        [HttpGet("Received/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetReceivedFriendRequestByID(int id)
        {
            ICollection<int> FriendRequest = repo.GetAllReceivedFriendRequestsForAccount(id);
            if (FriendRequest == null)
                return NotFound();
            return Ok(FriendRequest);
        }
        [HttpDelete("{firstId:int}/{secondId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteFriendRequest(int firstId, int secondId)
        {
            FriendRequest FriendRequest = repo.GetFriendRequest(firstId,secondId);
            if (FriendRequest == null)
                return NotFound("Check Id");
            if (!repo.DeleteFriendRequest(FriendRequest))
            {
                ModelState.AddModelError("", $"something went wrong deleting the record");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Logic;
using xZoneAPI.Models.Accounts;
using xZoneAPI.Repositories.AccountRepo;


namespace xZoneAPI.Controllers.AccountControllers
{
    [Route("api/v{version:apiVersion}/Friend")]
    //[Route("api/[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        IFriendRepository repo;
        private readonly IMapper mapper;
        IGamificationLogic gamificationLogic;
        public FriendController(IFriendRepository _repo, IMapper _mapper, IGamificationLogic gamificationLogic)
        {
            repo = _repo;
            mapper = _mapper;
            this.gamificationLogic = gamificationLogic;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddFriend([FromBody] FriendRequest FriendRequest)
        {
            if (FriendRequest == null)
            {
                return BadRequest(ModelState);
            }
            Friend friend = new Friend() { FirstId = FriendRequest.SenderId,
                SecondId = FriendRequest.ReceiverId};
            // TODO verify whether tasks exists or not            
            var OperationStatus = repo.AddFriend(friend);
            if (OperationStatus == null)
            {
                ModelState.AddModelError("", $"Something wrong in adding Friend");
                return StatusCode(500, ModelState);
            }
            gamificationLogic.checkForNewAchievements(FriendRequest.SenderId);
            gamificationLogic.checkForNewAchievements(FriendRequest.ReceiverId);
            return Ok(OperationStatus);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetFriends()
        {
            ICollection<Friend> objList = repo.GetAllFriends();
            return Ok(objList);
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetFriendByID(int id)
        {
            ICollection<Friend> Friend = repo.GetAllFriendsForAccount(id);
            if (Friend == null)
                return NotFound();
            return Ok(Friend);
        }

        [HttpDelete("{firstId:int}/{secondId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteFriend(int firstId, int secondId)
        {
            Friend Friend = repo.GetFriend(firstId,secondId);
            if (Friend == null)
                return NotFound("Check Id");
            if (!repo.DeleteFriend(Friend))
            {
                ModelState.AddModelError("", $"something went wrong deleting the record");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}

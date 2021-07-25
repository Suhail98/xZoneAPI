using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using xZoneAPI.Logic;
using xZoneAPI.Models.Posts;
using xZoneAPI.Models.Zones;
using xZoneAPI.Repositories.PostRepo;
using xZoneAPI.Repositories.ZoneRepo;

namespace xZoneAPI.Controllers.Posts
{
    [Route("api/v{version:apiVersion}/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private IPostRepository PostRepo;
        private IZoneMembersRepository ZoneMemberRepo;
        private readonly IMapper mapper;
        IGamificationLogic gamificationLogic;
        public PostController(IPostRepository postRepository, IZoneMembersRepository zoneMembersRepository, IMapper _mapper, IGamificationLogic gamificationLogic)
        {
            PostRepo = postRepository;
            mapper = _mapper;
            ZoneMemberRepo = zoneMembersRepository;
            this.gamificationLogic = gamificationLogic;
        }

        [HttpPost("writepost")]
        public IActionResult WritePost([FromBody] PostDto post)
        {
            if ( post == null) { return BadRequest(ModelState); }
            var xPost = mapper.Map<Post>(post);
            var OperationStatus = PostRepo.AddPost(xPost);
            if (OperationStatus == null)
            {
                ModelState.AddModelError("", $"Something wrong in adding Post");
                return StatusCode(500, ModelState);
            }
            var achievments = gamificationLogic.checkForNewAchievements(post.WriterId);
            return Ok(xPost);
        }


        [HttpDelete("Delete/{PostId:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteProject(int PostId)
        {
            var xPost = PostRepo.GetPost(PostId);
            if (xPost == null)
            {
                return NotFound();
            }
            var OperationStatus = PostRepo.DeletePost(xPost);
            if (!OperationStatus)
            {
                ModelState.AddModelError("", $"Something wrong in deleting {xPost.Id} Post");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPatch]
        public IActionResult EditPost([FromBody] Post post)
        {
            if (post == null)
            {
                return BadRequest(ModelState);
            }
            var updPost = PostRepo.UpdatePost(post);
            return Ok(updPost);
        }

        [HttpGet("{ZoneId:int}/{WriterId}")]
        public IActionResult GetZoneMemberPosts(int ZoneId, int WriterId)
        {
            ZoneMember zoneMember = ZoneMemberRepo.GetZoneMember(WriterId,ZoneId);
            
            if ( zoneMember != null && zoneMember.ZoneId != ZoneId )
            {
                return BadRequest(ModelState);
            }

            return Ok(PostRepo.GetAllPostsForZoneMember(ZoneId, WriterId));
        }

        [HttpGet("zoneposts/{ZoneId:int}")]
        public IActionResult GetZonePosts(int ZoneId)
        {
            var posts = PostRepo.GetAllPostsForZone(ZoneId);
            return Ok(posts);
        }





    }
}

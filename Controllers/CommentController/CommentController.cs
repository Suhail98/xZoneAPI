using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.CommentModel;
using xZoneAPI.Repositories.PostRepo;

namespace xZoneAPI.Controllers.CommentController
{
    [Route("api/v{version:apiVersion}/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private ICommentRepository CommentRepo;
        private readonly IMapper mapper;
        public CommentController(ICommentRepository commentRepository, IMapper mapper)
        {
            CommentRepo = commentRepository;
            this.mapper = mapper;
        }


        [HttpPost("WriteComment")]
        public IActionResult WriteComment([FromBody] CommentDto commentDto)
        {
            if (commentDto == null) { return BadRequest(ModelState); }
            var Comment = mapper.Map<Comment>(commentDto);
            var OperationStatus = CommentRepo.AddComment(Comment);
            if (OperationStatus == null)
            {
                ModelState.AddModelError("", $"Something wrong in adding Comment");
                return StatusCode(500, ModelState);
            }
            return Ok(Comment);
        }


        [HttpDelete("Delete/{CommentId:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteProject(int CommentId)
        {
            var Comment = CommentRepo.GetComment(CommentId);
            if (Comment == null)
            {
                return NotFound();
            }
            var OperationStatus = CommentRepo.DeleteComment(Comment);
            if (!OperationStatus)
            {
                ModelState.AddModelError("", $"Something wrong in deleting {Comment.Id} Comment");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPatch("updateComment")]
        public IActionResult UpdateComment([FromBody] CommentDto commentDto)
        {
            var Comment = mapper.Map<Comment>(commentDto);

            if (Comment == null)
            {
                return BadRequest(ModelState);
            }
            var comment = CommentRepo.UpdateComment(Comment);
            return Ok(comment);
        }

        [HttpGet("postComments/{PostId:int}")]
        public IActionResult GetPostComments(int PostId)
        {
            var comments = CommentRepo.GetAllCommentsForPost(PostId);
            return Ok(comments);
        }

    }
}

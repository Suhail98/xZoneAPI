using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.CommentModel;

namespace xZoneAPI.Repositories.PostRepo
{
    public interface ICommentRepository
    {
        Comment AddComment(Comment Comment);
        ICollection<Comment> GetAllCommentsForPost(int PostId);
        bool DeleteComment(Comment Comment);
        bool UpdateComment(Comment Comment);
        public bool Save();
        Comment GetComment(int CommentId);
    }
}

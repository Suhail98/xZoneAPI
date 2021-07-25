using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Data;
using xZoneAPI.Models.CommentModel;

namespace xZoneAPI.Repositories.PostRepo
{
    public class CommentRepository : ICommentRepository
    {
        ApplicationDBContext db;

        public CommentRepository(ApplicationDBContext _db)
        {
            db = _db;
        }

        public Comment AddComment(Comment Comment)
        {
            Comment.Date = DateTime.Now;
            db.Comments.Add(Comment);
            Save();
            return Comment;
        }

        public bool DeleteComment(Comment Comment)
        {
            db.Comments.Remove(Comment);
            return Save();
        }
        public ICollection<Comment> GetAllCommentsForPost(int PostId)
        {
            List<Comment> comments = db.Comments.Include(u=>u.Writer).Where(cmnt => cmnt.PostId == PostId).ToList();
            return comments;
        }
        public bool UpdateComment(Comment Comment)
        {
            db.Comments.Update(Comment);
            return Save();
        }
        public bool Save()
        {
            return db.SaveChanges() >= 0;
        }

        public Comment GetComment(int CommentId)
        {
            return db.Comments.Find(CommentId);
        }

        

    }
}

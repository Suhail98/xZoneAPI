using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Data;
using xZoneAPI.Models.Posts;

namespace xZoneAPI.Repositories.PostRepo
{
    public class PostRepository : IPostRepository
    {
        ApplicationDBContext db;

        public PostRepository(ApplicationDBContext _db)
        {
            db = _db;
        }

        public Post AddPost(Post Post)
        {
            Post.Date = DateTime.Now;
            db.Posts.Add(Post);
            Save();
            return Post;
        }

        public bool DeletePost(Post Post)
        {
            db.Posts.Remove(Post);
            return Save();
        }
        public ICollection<Post> GetAllPostsForZone(int ZoneId)
        {
            return db.Posts.ToList();
        }

        public ICollection<Post> GetAllPostsForZoneMember(int ZoneId, int WritertId)
        {
            var posts = db.Posts.Where(p => p.ZoneId == ZoneId && p.WriterId == WritertId).ToList();
            return posts;
        }

        public bool Save()
        {
            return db.SaveChanges() >= 0;
        }

        public bool UpdatePost(Post Post)
        {
            db.Posts.Update(Post);
            return Save();
        }

        public Post GetPost(int PostId)
        {
            var xPost = db.Posts.SingleOrDefault(p => p.Id == PostId);
            return xPost;
        }

        public int GetNumOfPostsForUser(int userId)
        {
            return db.Posts.Where(u => u.WriterId == userId).Count();
        }
    }
}

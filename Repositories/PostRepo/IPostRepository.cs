using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Posts;

namespace xZoneAPI.Repositories.PostRepo
{
    public interface IPostRepository
    {
        Post AddPost(Post Post);
        //Post FindPostById(int Id);
        ICollection<Post> GetAllPostsForZone(int ZoneId);
        ICollection<Post> GetAllPostsForZoneMember(int ZoneId, int WritertId);
        int GetNumOfPostsForUser(int userId);
        bool DeletePost(Post Post);
        bool UpdatePost(Post Post);
        public bool Save();
        public Post GetPost(int PostId);

    }
}

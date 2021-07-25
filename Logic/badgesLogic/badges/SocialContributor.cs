using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Repositories.AccountRepo;
using xZoneAPI.Repositories.PostRepo;
using xZoneAPI.Repositories.TaskRepo;

namespace xZoneAPI.badgesLogic.badges
{
    public class SocialContributor : AbstractBadge
    {
        
        IPostRepository postRepo;
        public SocialContributor(IPostRepository postRepo)
        {
            Id = 3;
            this.postRepo = postRepo;
        }
        public override bool evaluate(int userID)
        {
            return postRepo.GetNumOfPostsForUser(userID) >= 2? true : false;
        }
    }
}

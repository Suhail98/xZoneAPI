using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.badgesLogic.badges;
using xZoneAPI.Logic.badgesLogic.badges;
using xZoneAPI.Repositories.AccountRepo;
using xZoneAPI.Repositories.PostRepo;
using xZoneAPI.Repositories.TaskRepo;

namespace xZoneAPI.badgesLogic
{
    public class BadgesSetFactory : IBadgesSetFactory
    {
        ITaskRepository _taskRepo;
        IAccountRepo accountRepo;
        IPostRepository postRepo;
        IFriendRepository friendRepo;
        IProjectTaskRepository _projectTaskRepo;
        IAccountZoneTaskRepo _accountZoneTaskRepo;

        public BadgesSetFactory(ITaskRepository appTaskRepo, IAccountZoneTaskRepo accountZoneTaskRepo, IProjectTaskRepository projectTaskRepository, IAccountRepo accountRepo, IPostRepository postRepo, IFriendRepository friendRepo)
        {
            _taskRepo = appTaskRepo;
            _accountZoneTaskRepo = accountZoneTaskRepo;
            _projectTaskRepo = projectTaskRepository;
            this.accountRepo = accountRepo;
            this.postRepo = postRepo;
            this.friendRepo = friendRepo;
        }

        public List<AbstractBadge> createFullAchievementSet()
        {
            List<AbstractBadge> achievements = new List<AbstractBadge>();
            achievements.Add(new FiveTasksBadge(_taskRepo, _projectTaskRepo, _accountZoneTaskRepo));
            achievements.Add(new ExpertBadge(_taskRepo, _projectTaskRepo, _accountZoneTaskRepo));
            achievements.Add(new GoodFriends(friendRepo));
            achievements.Add(new HighestRank(accountRepo));
            achievements.Add(new SocialContributor(postRepo));

            return achievements;
        }



    }
}

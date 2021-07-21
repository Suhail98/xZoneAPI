using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.badgesLogic;
using xZoneAPI.Models.Accounts;
using xZoneAPI.Repositories.AccountBadges;
using xZoneAPI.Repositories.AccountRepo;
using xZoneAPI.Repositories.Ranks;
using xZoneAPI.Repositories.TaskRepo;
using static xZoneAPI.Models.Accounts.Account;

namespace xZoneAPI.Logic
{
    public class GamificationLogic : IGamificationLogic
    {

        List<AbstractBadge> badges;
        IAccountRepo accountRepo;
        IAccountBadgeRepo accountBadgeRepo;
        ITaskRepository taskRepo;
        Account account;

        public GamificationLogic(IBadgesSetFactory factory, IAccountBadgeRepo accountBadgeRepo, ITaskRepository taskRepo, IAccountRepo accountRepo)
        {
            badges = factory.createFullAchievementSet();
            this.accountBadgeRepo = accountBadgeRepo;
            this.taskRepo = taskRepo;
            this.accountRepo = accountRepo;
        }
        public AchievmentsNotifications checkForNewAchievements(int userID)
        {
            AchievmentsNotifications achievmentsNotifications = new AchievmentsNotifications();
            account = accountRepo.GetAccountWithItsBadges(userID);
            achievmentsNotifications.badges = getNewBadges(userID);
            achievmentsNotifications.newRank = getNewRank(account);
            return achievmentsNotifications;
        }

        private RankType? getNewRank(Account account)
        {
            int numOfActiveDays = taskRepo.GetActiveDays(account.Id);
            RankType newRank = (RankType)(numOfActiveDays / 2);
            RankType oldRank = account.Rank;
            account.Rank = newRank;
            accountRepo.UpdateAccount(account);
            return oldRank == newRank ? null : newRank;
        }

        private List<int> getNewBadges(int userID)
        {
            List<int> badgesId = new List<int>();
            foreach (AbstractBadge badge in badges)
            {
                if (account.Badges.SingleOrDefault(u => u.BadgeID == badge.Id) == null &&
                    badge.evaluate(userID))
                {
                    badgesId.Add(badge.Id);
                    AccountBadge accountBadge = new AccountBadge(userID, badge.Id);
                    accountBadgeRepo.AddAccountBadge(accountBadge);
                }

            }
            return badgesId;
        }
    }
}

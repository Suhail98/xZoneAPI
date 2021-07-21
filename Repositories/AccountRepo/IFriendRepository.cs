using System.Collections.Generic;
using xZoneAPI.Models.Accounts;

namespace xZoneAPI.Repositories.AccountRepo
{
    public interface IFriendRepository
    {
        Friend AddFriend(Friend friend);
        bool DeleteFriend(Friend friend);
        ICollection<Friend> GetAllFriends();
        ICollection<Friend> GetAllFriendsForAccount(int Id);
        public ICollection<Account> GetAllFriendsAccountForAccount(int Id);
        Friend GetFriend(int firstId, int secondId);
        ICollection<int> GetFriendsId(int Id);
        bool Save();
    }
}
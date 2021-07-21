using System.Collections.Generic;
using xZoneAPI.Models.Accounts;

namespace xZoneAPI.Repositories.AccountRepo
{
    public interface IFriendRequestRepository
    {
        FriendRequest AddFriendRequest(FriendRequest FriendRequest);
        bool DeleteFriendRequest(FriendRequest FriendRequest);
        ICollection<FriendRequest> GetAllFriendRequests();
        ICollection<int> GetAllReceivedFriendRequestsForAccount(int Id);
        ICollection<int> GetAllSentFriendRequestsForAccount(int Id);
        FriendRequest GetFriendRequest(int firstId, int secondId);
        bool Save();
    }
}
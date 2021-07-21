using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Data;
using xZoneAPI.Models.Accounts;

namespace xZoneAPI.Repositories.AccountRepo
{
    public class FriendRequestRepository : IFriendRequestRepository
    {
        private readonly ApplicationDBContext _db;
        public FriendRequestRepository(ApplicationDBContext db)
        {
            _db = db;
        }
        public FriendRequest AddFriendRequest(FriendRequest FriendRequest)
        {
            _db.FriendRequests.Add(FriendRequest);
            Save();
            return FriendRequest;
        }
        public FriendRequest GetFriendRequest(int firstId, int secondId)
        {
            FriendRequest FriendRequest = _db.FriendRequests.SingleOrDefault(f => f.SenderId == firstId && f.ReceiverId == secondId);
            return FriendRequest;
        }
        public ICollection<FriendRequest> GetAllFriendRequests()
        {
            return _db.FriendRequests.ToList();
        }
        public ICollection<int> GetAllSentFriendRequestsForAccount(int Id)
        {
            return _db.FriendRequests.Where(u => u.SenderId == Id).Select(u => u.ReceiverId).ToList();
        }
        public ICollection<int> GetAllReceivedFriendRequestsForAccount(int Id)
        {
            return _db.FriendRequests.Where(u => u.ReceiverId == Id).Select(u => u.SenderId).ToList();
        }
        public bool DeleteFriendRequest(FriendRequest FriendRequest)
        {
            _db.FriendRequests.Remove(FriendRequest);
            return Save();
        }
        public bool Save()
        {
            return _db.SaveChanges() >= 0;
        }
    }
}


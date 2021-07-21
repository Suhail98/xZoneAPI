using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Data;
using xZoneAPI.Models.Accounts;

namespace xZoneAPI.Repositories.AccountRepo
{
    public class FriendRepository : IFriendRepository
    {
        private readonly ApplicationDBContext _db;
        public FriendRepository(ApplicationDBContext db)
        {
            _db = db;
        }
        public Friend AddFriend(Friend friend)
        {
            _db.Friends.Add(friend);
            Save();
            return friend;
        }
        public Friend GetFriend(int firstId, int secondId)
        {
            Friend friend = _db.Friends.SingleOrDefault(f => f.FirstId == firstId && f.SecondId == secondId || (f.FirstId == secondId && f.SecondId == firstId));
            return friend;
        }
        public ICollection<Friend> GetAllFriends()
        {
            return _db.Friends.ToList();
        }
        public ICollection<Friend> GetAllFriendsForAccount(int Id)
        {
            return _db.Friends.Include(u=>u.First).Include(u=>u.Second).Where(u => u.FirstId == Id || u.SecondId == Id).ToList();
        }
        public ICollection<Account> GetAllFriendsAccountForAccount(int Id)
        {
            return _db.Friends.Where(u => u.FirstId == Id || u.SecondId == Id)
                .Include(u=>u.First)
                .Include(u=>u.Second)
                .Select(u=>u.FirstId != Id?u.First:u.Second)
                .Select(u=> new Account(){ Id = u.Id,UserName = u.UserName})
                .ToList();
        }
        public bool DeleteFriend(Friend friend)
        {
            _db.Friends.Remove(friend);
            return Save();
        }
        public bool Save()
        {
            return _db.SaveChanges() >= 0;
        }
        public ICollection<int> GetFriendsId(int Id)
        {
            return _db.Friends.Where(u => u.FirstId == Id || u.SecondId == Id).Select(f => Id == f.FirstId ? f.FirstId : f.SecondId).ToList();
        }
    }
}

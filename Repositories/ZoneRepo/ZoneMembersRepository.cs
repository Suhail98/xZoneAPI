using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Data;
using xZoneAPI.Models.Zones;

namespace xZoneAPI.Repositories.ZoneRepo
{
    public class ZoneMembersRepository : IZoneMembersRepository
    {
        ApplicationDBContext db;
        private readonly AppSettings appSettings;
        IZoneRepository ZoneRepository;

        public ZoneMembersRepository(ApplicationDBContext _db, IOptions<AppSettings> _appSettings)
        {
            db = _db;
            appSettings = _appSettings.Value;
        }
        public bool AddZoneMember(ZoneMember Member)
        {
            db.ZoneMembers.Add(Member);
            Zone zone = db.Zones.SingleOrDefault(u => Member.ZoneId == u.Id);
            zone.NumOfMembers += 1;
            db.Zones.Update(zone);
            return Save();
        }

        public ICollection<ZoneMember> GetAllZoneMembers(int ZoneId)
        {
            return db.ZoneMembers.Where(zm => zm.ZoneId == ZoneId).ToList();
        }
        public ICollection<ZoneMember> GetAllZoneMembers()
        {
            return db.ZoneMembers.ToList();
        }
        public bool IsAdmin(ZoneMember Member)
        {
            return (Member.Role == ZoneMember.Roles.Admin);
        }

        public bool RemoveZoneMember(ZoneMember Member)
        {
            db.ZoneMembers.Remove(Member);
            Zone zone = db.Zones.SingleOrDefault(u => Member.ZoneId == u.Id);
            zone.NumOfMembers -= 1;
            db.Zones.Update(zone);
            return Save();
        }

        public bool UpdateZoneMember(ZoneMember Member)
        {
            db.ZoneMembers.Update(Member);
            return Save();
        }

        public bool Save()
        {
            return db.SaveChanges() >= 0 ;
        }

        public ZoneMember GetZoneMember(int AccountMemberId, int zoneId)
        {
            ZoneMember zoneMember = db.ZoneMembers.FirstOrDefault(zm => zm.ZoneId == zoneId && zm.AccountId == AccountMemberId );
            return zoneMember;
        }
        public ICollection<int> GetAccountZonesId(int AccountMemberId)
        {
            ICollection<int> zonesId = db.ZoneMembers.Where(zm => zm.AccountId == AccountMemberId).Select(zm => zm.ZoneId).ToList();
            return zonesId;
        }
      
        ZoneMember IZoneMembersRepository.AddCompletedTask(int accountID, int zoneId)
        {
            ZoneMember zoneMember = GetZoneMember(accountID, zoneId);
            zoneMember.NumOfCompletedTasks += 1;
            UpdateZoneMember(zoneMember);
            Save();
            return zoneMember;
        }
    }
}

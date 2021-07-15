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

        public ZoneMembersRepository(ApplicationDBContext _db, IOptions<AppSettings> _appSettings)
        {
            db = _db;
            appSettings = _appSettings.Value;
        }
        public bool AddZoneMember(ZoneMember Member)
        {
            db.ZoneMembers.Add(Member);
            return Save();
        }

        public ICollection<ZoneMember> GetAllZoneMembers(int ZoneId)
        {
            return db.ZoneMembers.Where(zm => zm.ZoneId == ZoneId).ToList();
        }

        public bool IsAdmin(ZoneMember Member)
        {
            return (Member.Role == ZoneMember.Roles.Admin);
        }

        public bool RemoveZoneMember(ZoneMember Member)
        {
            db.ZoneMembers.Remove(Member);
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

        public ZoneMember GetZoneMember(int AccountMemberId)
        {
            ZoneMember zoneMember = db.ZoneMembers.FirstOrDefault(zm => zm.AccountId == AccountMemberId);
            return zoneMember;
        }

    }
}

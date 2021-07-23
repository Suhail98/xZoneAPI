using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Zones;

namespace xZoneAPI.Repositories.ZoneRepo
{
    public interface IZoneMembersRepository
    {
        public bool AddZoneMember(ZoneMember Member);
        public bool UpdateZoneMember(ZoneMember Member);
        public bool RemoveZoneMember(ZoneMember Member);
        public bool IsAdmin(ZoneMember Memeber);
        public ICollection<ZoneMember> GetAllZoneMembers(int ZoneId);
        public ICollection<ZoneMember> GetAllZoneMembers();
        public bool Save();
        public ZoneMember GetZoneMember(int AccountMemberId, int zoneId);
        ICollection<int> GetAccountZonesId(int AccountMemberId);

        ZoneMember AddCompletedTask(int accountID, int zoneId);
        //        public bool RemovePost(ZoneMember Member, int PostId);





    }
}

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
        public bool Save();


//        public bool RemovePost(ZoneMember Member, int PostId);





    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Skills;
using xZoneAPI.Models.Zones;

namespace xZoneAPI.Repositories.ZoneRepo
{
    public interface IZoneSkillRepository
    {
        public bool AddZoneSkill(ZoneSkill zoneSkill);
        public bool DeleteZoneSkill(ZoneSkill zoneSkill);

        public bool Save();
        ICollection<ZoneSkill> GetAllZoneSkills();
        List<Skill> GetAllSkillsForZone(int zoneId);

        public ZoneSkill GetZoneSkill(int zoneId, int skillId);
        public List<Zone> GetPublicZonesForSkill(ICollection<int> skillsId, int userId);
    }
}

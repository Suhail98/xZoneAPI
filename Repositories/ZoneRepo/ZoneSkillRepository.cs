using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using xZoneAPI.Data;
using xZoneAPI.Models.Skills;
using xZoneAPI.Models.Zones;

namespace xZoneAPI.Repositories.ZoneRepo
{
    public class ZoneSkillRepository : IZoneSkillRepository
    {
        ApplicationDBContext db;
        private readonly AppSettings appSettings;

        public ZoneSkillRepository(ApplicationDBContext _db, IOptions<AppSettings> _appSettings)
        {
            db = _db;
            appSettings = _appSettings.Value;
        }
        public bool AddZoneSkill(ZoneSkill zoneSkill)
        {
            db.ZoneSkills.Add(zoneSkill);
            return Save();
        }

        public bool DeleteZoneSkill(ZoneSkill zoneSkill)
        {
            db.ZoneSkills.Remove(zoneSkill);
            return Save();
        }

        public bool Save()
        {
            return db.SaveChanges() >= 0;
        }

        public ICollection<ZoneSkill> GetAllZoneSkills()
        {
            return db.ZoneSkills.ToList();
        }

        public List<Skill> GetAllSkillsForZone(int zoneId)
        {
            List<ZoneSkill> zoneSkills = db.ZoneSkills.Include(u=>u.Skill).Where(zs => zs.ZoneId == zoneId).ToList();
            List<Skill> skills = zoneSkills.Select(u => u.Skill).ToList();
            return skills;
        }
        public ICollection<Zone> GetZonesForSkill(ICollection<int> skillsId)
        {
            ICollection<Zone> zones = db.ZoneSkills
                .Include(u=> u.Zone)
                .Where(u => skillsId.SingleOrDefault(x=>u.SkillId == x) != null).Select(u=>u.Zone).ToList();
            return zones;
        }
        public ZoneSkill GetZoneSkill(int zoneId, int skillId)
        {
            var zoneSkill = db.ZoneSkills.SingleOrDefault(zs => zs.SkillId == skillId && zs.ZoneId == zoneId);
            return zoneSkill;
        }


    }
}

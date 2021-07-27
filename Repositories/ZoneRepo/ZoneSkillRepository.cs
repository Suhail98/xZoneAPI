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
        public List<Zone> GetPublicZonesForSkill(ICollection<int> skillsId, int userId)
        {
            List<Zone> zones = new List<Zone>();
            foreach(int skillId in skillsId)
            {
                List<ZoneSkill> zoneSkills = db.ZoneSkills.Include(u => u.Zone).Where(u => u.SkillId == skillId
                && u.Zone.Privacy == Zone.PrivacyType.Public).ToList();
                List<Zone> userZones = db.ZoneMembers.Where(u => u.AccountId == userId).Select(u=>u.Zone).ToList();
                foreach(ZoneSkill zoneSkill in zoneSkills)
                {
                    if(userZones.SingleOrDefault(u=>u.Id == zoneSkill.Zone.Id) == null)
                        zones.Add(zoneSkill.Zone);
                }
            }
            
            return zones;
        }
        public ZoneSkill GetZoneSkill(int zoneId, int skillId)
        {
            var zoneSkill = db.ZoneSkills.SingleOrDefault(zs => zs.SkillId == skillId && zs.ZoneId == zoneId);
            return zoneSkill;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Accounts;
using xZoneAPI.Models.Zones;
using xZoneAPI.Repositories.AccountRepo;
using xZoneAPI.Repositories.ZoneRepo;

namespace xZoneAPI.Recommenders
{
   
    public class ZoneRecommender
    {
        IZoneRepository zoneRepo;
        IZoneSkillRepository zoneSkillRepo;
        IAccountSkillRepo accountSkillRepo;
        public ZoneRecommender(IZoneRepository zoneRepo, IAccountSkillRepo accountSkillRepo, IZoneSkillRepository zoneSkillRepo)
        {
            this.zoneRepo = zoneRepo;
            this.accountSkillRepo = accountSkillRepo;
            this.zoneSkillRepo = zoneSkillRepo;
        }
        /*
        ICollection<Zone> getTenMaxRecommendedZones(ICollection<int> skillsIds, ICollection<Zone> zones)
        {
            foreach(Zone zone in zones)
            {

            }
        }
        
        public ICollection<Zone> getRecommendedZones(int userId)
        {
            ICollection<int> skillsIds = accountSkillRepo.GetAccountSkillsId(userId);
            ICollection<Zone> zones = zoneSkillRepo.GetZonesForSkill(skillsIds);
            SortedDictionary<double, Zone> openWith =
           new SortedDictionary<double, Zone>();
        }
    */}
}

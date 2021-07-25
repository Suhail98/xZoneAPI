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

    public class ZoneRecommender : IZoneRecommender
    {
        IAccountRepo accountRepo;
        IZoneRepository zoneRepo;
        IZoneSkillRepository zoneSkillRepo;
        IAccountSkillRepo accountSkillRepo;
        public ZoneRecommender(IZoneRepository zoneRepo, IAccountSkillRepo accountSkillRepo, IZoneSkillRepository zoneSkillRepo, IAccountRepo accountRepo)
        {
            this.zoneRepo = zoneRepo;
            this.accountSkillRepo = accountSkillRepo;
            this.zoneSkillRepo = zoneSkillRepo;
            this.accountRepo = accountRepo;
        }
        double cosineSim(ICollection<int> userSkills, Zone zone, string userLocation)
        {
            ICollection<int> secondSkills = (zone.ZoneSkills.Select(u => u.SkillId)).ToList();
            double top = 0;
            if (zone.NumOfMembers > 0 && zone.NumOfAdminLocation / zone.NumOfMembers >= 0.3) zone.Location = zone.AdminLocation;
            if (zone.Location == userLocation) top += 1;
            foreach (int skill in userSkills)
            {
                foreach (int skill2 in secondSkills)
                    if (skill == skill2) top += 1;
            }
            double bot = Math.Sqrt(userSkills.Count()+1) * Math.Sqrt(secondSkills.Count()+1);
            return top / bot;
        }

        List<Zone> getTenMaxRecommendedZones(ICollection<int> skillsIds, ICollection<Zone> zones, string userLocation)
        {
            SortedDictionary<double, Zone> simZones =
            new SortedDictionary<double, Zone>((Comparer<double>.Create((x, y) => y.CompareTo(x))));
            foreach (Zone zone in zones)
            {
                
                simZones.Add(cosineSim(skillsIds, zone,userLocation), zone);
            }
            List<Zone> result = new List<Zone>();
            foreach (KeyValuePair<double, Zone> pair in simZones)
            {
                result.Add(pair.Value);
            }
            return result;
        }

        public List<Zone> getRecommendedZones(int userId)
        {
            string location = accountRepo.FindAccountById(userId).location;
            ICollection<int> skillsIds = accountSkillRepo.GetAccountSkillsId(userId);
            ICollection<Zone> zones = zoneSkillRepo.GetZonesForSkill(skillsIds);
            List<Zone> result = getTenMaxRecommendedZones(skillsIds, zones,location);
            return result;
        }
    }
}

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
        IZoneRepository zoneRepo;
        IZoneSkillRepository zoneSkillRepo;
        IAccountSkillRepo accountSkillRepo;
        public ZoneRecommender(IZoneRepository zoneRepo, IAccountSkillRepo accountSkillRepo, IZoneSkillRepository zoneSkillRepo)
        {
            this.zoneRepo = zoneRepo;
            this.accountSkillRepo = accountSkillRepo;
            this.zoneSkillRepo = zoneSkillRepo;
        }
        double cosineSim(ICollection<int> firstSkills, ICollection<int> secondSkills)
        {
            double top = 0;
            foreach (int skill in firstSkills)
            {
                foreach (int skill2 in secondSkills)
                    if (skill == skill2) top += 1;
            }
            double bot = Math.Sqrt(firstSkills.Count()) * Math.Sqrt(secondSkills.Count());
            return top / bot;
        }

        List<Zone> getTenMaxRecommendedZones(ICollection<int> skillsIds, ICollection<Zone> zones)
        {
            SortedDictionary<double, Zone> simZones =
            new SortedDictionary<double, Zone>((Comparer<double>.Create((x, y) => y.CompareTo(x))));
            foreach (Zone zone in zones)
            {
                ICollection<int> secondSkills = (zone.ZoneSkills.Select(u=>u.SkillId)).ToList();
                simZones.Add(cosineSim(skillsIds, secondSkills), zone);
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
            ICollection<int> skillsIds = accountSkillRepo.GetAccountSkillsId(userId);
            ICollection<Zone> zones = zoneSkillRepo.GetZonesForSkill(skillsIds);
            List<Zone> result = getTenMaxRecommendedZones(skillsIds, zones);
            return result;
        }
    }
}

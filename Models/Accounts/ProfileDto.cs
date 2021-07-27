using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.RoadmapModel;
using xZoneAPI.Models.Zones;
using static xZoneAPI.Models.Accounts.Account;

namespace xZoneAPI.Models.Accounts
{
    public class ProfileDto
    {
        public int Id { get; set; }
        //public string Email { get; set; }
        public string UserName { get; set; }

        public string email { get; set; }
        public string? bio { get; set; }
        public RankType Rank { get; set; }
        public ICollection<Account> Friends { get; set; }
        public ICollection<AccountSkill> Skills { get; set; }
        public ICollection<AccountBadge> Badges { get; set; }                    
        public ICollection<Roadmap> Roadmaps { get; set; }
        public ICollection<ZoneMember> Zones { get; set; }


    }
}

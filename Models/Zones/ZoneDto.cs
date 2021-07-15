using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static xZoneAPI.Models.Zones.Zone;

namespace xZoneAPI.Models.Zones
{
    public class ZoneDto
    {
        
        public string Name { get; set; }
        public string Description { get; set; }
        public PrivacyType Privacy { get; set; } = PrivacyType.Public;
        public ICollection<ZoneSkill> ZoneSkills { get; set; } 
        public string JoinCode { get; set; } = "";
    }
}

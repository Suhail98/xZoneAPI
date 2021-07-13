using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xZoneAPI.Models.Zones
{
    public class ZoneDto
    {
        
        public string Name { get; set; }
        public string Description { get; set; }
        public enum PrivacyType { Private, Public }
        public PrivacyType Privacy { get; set; }
        public ICollection<ZoneSkill> ZoneSkills { get; set; }
        public string JoinCode { get; set; } = "";
    }
}

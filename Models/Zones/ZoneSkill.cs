using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Skills;

namespace xZoneAPI.Models.Zones
{
    public class ZoneSkill
    {
        [ForeignKey("Zone")]
        public int ZoneId { get; set; }
        [ForeignKey("Skill")]
        public int SkillId { get; set; }
        public Zone Zone { get; set; }
        public Skill Skill { get; set; }
    }
}

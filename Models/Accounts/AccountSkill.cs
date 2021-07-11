using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Skills;

namespace xZoneAPI.Models.Accounts
{
    public class AccountSkill
    {
        [ForeignKey("Account")]
        public int AccountID {get;set;}
        [ForeignKey("Skill")]
        public int SkillID { get; set; }

        public Account Account { get; set; }
        public Skill Skill { get; set; }
    }
}

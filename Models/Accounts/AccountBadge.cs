using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Badges;
using xZoneAPI.Models.Skills;

namespace xZoneAPI.Models.Accounts
{
    public class AccountBadge
    {
        [ForeignKey("Account")]
        public int AccountID {get;set;}
        [ForeignKey("Badge")]
        public int BadgeID { get; set; }

        public Account Account { get; set; }
        public Badge Badge { get; set; }
    }
}

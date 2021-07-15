using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Accounts;

namespace xZoneAPI.Models.Zones
{
    public class ZoneMember
    {
        [ForeignKey("Zone")]
        public int ZoneId{ get; set; }
        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public enum Roles { Member,Admin }
        public Roles Role { get; set; } = Roles.Member;
        public int Score { get; set; }
        public int NumOfCompletedTasks { get; set; }
        public Zone Zone { get; set; }
        public Account Account { get; set; }

        public ZoneMember(int zoneId, int accountId, Roles role)
        {
            ZoneId = zoneId;
            AccountId = accountId;
            Role = role;
        }
    }
}

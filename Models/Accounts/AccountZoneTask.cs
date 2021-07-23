using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.TaskModel;

namespace xZoneAPI.Models.Accounts
{
    public class AccountZoneTask
    {
        [ForeignKey("Account")]
        public int AccountID { get; set; }
        [ForeignKey("ZoneTask")]
        public int ZoneTaskID { get; set; }

        public Account Account { get; set; }
        public ZoneTask ZoneTask { get; set; }

        public DateTime? CompleteDate { get; set; }
    }
}

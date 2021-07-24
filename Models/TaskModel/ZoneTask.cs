using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Accounts;
using xZoneAPI.Models.Zones;

namespace xZoneAPI.Models.TaskModel
{
    public class ZoneTask : AbstractTask
    {
        [Required]
        [ForeignKey("Zone")]
        public int ZoneId { get; set; }
        public Zone Zone { get; set; }
        public ICollection<AccountZoneTask> ZoneTasks { get; set; }

        public DateTime publishDate { get; set; }

    }
}

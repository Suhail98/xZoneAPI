using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Accounts;

namespace xZoneAPI.Models.TaskModel
{
    public class AppTask : AbstractTask
    {

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public Account User { get; set; }
        public DateTime? CompleteDate { get; set; }

    }
}

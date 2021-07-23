using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using xZoneAPI.Models.Accounts;

namespace xZoneAPI.Models.TaskModel
{
    public class ZoneTaskDto
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
       // public int? userID { get; set; }
        public int? Priority { get; set; }
        public AppTask? Parent { get; set; }
        public int? parentID { get; set; }
        //  public int skillID { get; set; }
        //  public Skill skill { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? Remainder { get; set; }
        public int ZoneId { get; set; }
    }
}

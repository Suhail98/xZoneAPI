using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace xZoneAPI.Models.TaskModel
{
    public class TaskDto
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int? userID { get; set; }
        public int? Priority { get; set; }
        public AppTask? Parent { get; set; }
        public int? parentID { get; set; }
      //  public int skillID { get; set; }
      //  public Skill skill { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? Remainder { get; set; }
        public DateTime? CompleteDate { get; set; }
    }
}

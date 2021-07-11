using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.ProjectTaskModel;

namespace xZoneAPI.Models.TaskModel
{
    public class ProjectTaskDto
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int SectionId { get; set; }
        public int? Priority { get; set; }
        public ProjectTask? Parent { get; set; }
        public int? parentID { get; set; }
        //  public int skillID { get; set; }
        //  public Skill skill { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? Remainder { get; set; }
        public DateTime? CompleteDate { get; set; }
    }
}

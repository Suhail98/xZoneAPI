using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.SectionModel;
using xZoneAPI.Models.TaskModel;

namespace xZoneAPI.Models.ProjectTaskModel
{
    public class ProjectTask : AbstractTask
    {
        [ForeignKey("ParentSection")]
        public int SectionID { get; set; }
        public Section ParentSection  { get; set; }
        public DateTime? CompleteDate { get; set; }
    }
}

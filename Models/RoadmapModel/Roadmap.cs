using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Accounts;
using xZoneAPI.Models.ProjectModel;

namespace xZoneAPI.Models.RoadmapModel
{
    
    public class Roadmap
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("Owner")]
        public int OwnerID { get; set; }

        public Account Owner { get; set; }

        public string Description { get; set; }
        
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public Project Project  { get; set; }

        public DateTime? PublishDate { get; set; }

        public int NumberOfLikes { get; set; }
    }
}

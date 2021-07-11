using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace xZoneAPI.Models.ProjectModel
{
    public class ProjectDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int OwnerID { get; set; } 

    }
}

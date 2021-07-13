using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Accounts;
using xZoneAPI.Models.SectionModel;

namespace xZoneAPI.Models.ProjectModel
{
    public class Project
    {
        
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Section> Sections { get; set; }
        
        [ForeignKey("Owner")]
        public int? OwnerID { get; set; }
        
        public Account Owner { get; set; }

    }
}

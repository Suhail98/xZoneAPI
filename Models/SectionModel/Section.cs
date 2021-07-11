using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.ProjectModel;
using xZoneAPI.Models.ProjectTaskModel;

namespace xZoneAPI.Models.SectionModel
{
    public class Section
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("ParentProject")]
        public int ParentProjectID { get; set; }

        public Project ParentProject { get; set; }

        public ICollection<ProjectTask> ProjectTasks { get; set; }

        public Section(int _id, string _name)
        {
            Id = _id;
            Name = _name;
        }
        public Section(string _name)
        {
            Name = _name;
        }
        public Section() { }
    }
}

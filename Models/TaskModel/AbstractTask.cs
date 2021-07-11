using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace xZoneAPI.Models.TaskModel
{
    [NotMapped]
    public class AbstractTask
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public int? Priority { get; set; }
        [ForeignKey("Parent")]
        public int? parentID { get; set; }

        public AbstractTask? Parent { get; set; }
        //    public int skillID { get; set; }
        //    [ForeignKey("skilID")]
        //  public Skill skill { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? Remainder { get; set; }
        public DateTime? CompleteDate { get; set; }

    }
}

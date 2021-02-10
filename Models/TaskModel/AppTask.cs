using Models.Skill;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace xZoneAPI.Models.TaskModel
{
    public class AppTask
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        
        [Required]
        public string Name { get; set; }
        public int? Priority { get; set; }
        [ForeignKey("parentID")]
        public int? parentID { get; set; }
        
        public AppTask? Parent { get; set; }
    //    public int skillID { get; set; }
    //    [ForeignKey("skilID")]
      //  public Skill skill { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? Remainder { get; set; }
        public DateTime? CompleteDate { get; set; }

    }
}

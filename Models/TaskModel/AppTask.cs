using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Accounts;

namespace xZoneAPI.Models.TaskModel
{
    public class AppTask
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public Account user { get; set; }
        
        [Required]
        public string Name { get; set; }
        public int? Priority { get; set; }
        
        public int? parentID { get; set; }
        [ForeignKey("parentID")]
        public AppTask? Parent { get; set; }
    //    public int skillID { get; set; }
    //    [ForeignKey("skilID")]
      //  public Skill skill { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? Remainder { get; set; }
        public DateTime? CompleteDate { get; set; }

    }
}

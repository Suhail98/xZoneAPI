using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Posts;
using xZoneAPI.Models.TaskModel;

namespace xZoneAPI.Models.Zones
{
    public class Zone
    {
        [Key]
        public int Id{get; set;}
        public string Name { get; set; }
        public string Description {get; set;}
        public enum PrivacyType { Private, Public }
        public PrivacyType Privacy { get; set; }
        public string JoinCode { get; set; } = "";
        public int NumOfMembers { get; set; }

        public string AdminLocation { get; set; }
        
        public int NumOfAdminLocation { get; set; }
        [NotMapped]
        public string Location { get; set; }
        public ICollection<Post> Posts {get; set;}
        public ICollection<ZoneTask> Tasks { get; set; }
        public ICollection<ZoneMember> ZoneMembers { get; set; }
        
        public ICollection<ZoneSkill> ZoneSkills { get; set; }      
}
}

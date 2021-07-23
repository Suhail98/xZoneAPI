using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using xZoneAPI.Models.Skills;
using xZoneAPI.Models.Ranks;
using xZoneAPI.Models.Badges;
//using Microsoft.CodeAnalysis;
using xZoneAPI.Models.TaskModel;
using xZoneAPI.Models.ProjectModel;
using xZoneAPI.Models.RoadmapModel;
using xZoneAPI.Models.Zones;
/// <summary>
///  A class that represents ...
/// 
///  @see OtherClasses
///  @author your_name_here
/// </summary>
namespace xZoneAPI.Models.Accounts
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [NotMapped]
        public string Token { get; set; }
        public string? bio { get; set; }

        public string location { get; set; }

        public enum RankType { Bronze, Silver, Gold, Plat }
        
       // [ForeignKey("Rank")]
        //public int? RankID { get; set; }
        
        public RankType Rank { get; set; }
        public ICollection<AccountSkill> Skills { get; set; }
        public ICollection<AccountBadge> Badges { get; set; }
        public ICollection<Friend> Friends { get; set; }
        public ICollection<FriendRequest> FriendRequests { get; set; }
        public ICollection<AppTask> Tasks { get; set; }
        public ICollection<AccountZoneTask> ZoneTasks { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<Roadmap> Roadmaps { get; set; }
        public ICollection<ZoneMember> Zones { get; set; }

    }
}
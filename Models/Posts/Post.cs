using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Accounts;
using xZoneAPI.Models.Zones;

namespace xZoneAPI.Models.Posts
{
    public class Post
    {
        [Key]
        public int Id {get; set;}
        public string content {get; set;}
        [ForeignKey("Writer")]
        public int WriterId { get; set; }
        public Account Writer { get; set; }
        [ForeignKey("Zone")]
        public int ZoneId { get; set; }
        public Zone Zone { get; set; }

        
    }
}

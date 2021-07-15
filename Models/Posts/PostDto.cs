using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xZoneAPI.Models.Posts
{
    public class PostDto
    {
        public string content { get; set; }    
        public int WriterId { get; set; }
        public int ZoneId { get; set; }
    }
}

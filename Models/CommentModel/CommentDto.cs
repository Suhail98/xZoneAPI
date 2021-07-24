using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xZoneAPI.Models.CommentModel
{
    public class CommentDto
    {
        public string content { get; set; }
        public int WriterId { get; set; }
        public int PostId { get; set; }
    }
}

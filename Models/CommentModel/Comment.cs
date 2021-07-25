using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Accounts;
using xZoneAPI.Models.Posts;

namespace xZoneAPI.Models.CommentModel
{
    public class Comment 
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Post")]
        public int PostId { get; set; }
        public Post Post { get; set; }
        
        public string content { get; set; }

        [ForeignKey("Writer")]

        public int? WriterId { get; set; }
        public Account Writer { get; set; }

        public DateTime Date { get; set; }

    }
}

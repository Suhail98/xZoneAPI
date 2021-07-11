using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using xZoneAPI.Models.Accounts;

namespace xZoneAPI.Models.CommentModel
{
    [NotMapped]
    public class Comment
    {
        public int Id { get; set; }
        public int ParentID { get; set; }
        public Account Writer { get; set; }
        public string Content { get; set; }

        // votes

    }
}

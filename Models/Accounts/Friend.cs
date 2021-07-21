using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace xZoneAPI.Models.Accounts
{
    public class Friend
    {
        [ForeignKey("First")]
        public int FirstId { get; set; }
        [ForeignKey("Second")]
        public int SecondId { get; set; }

        public Account First { get; set; }
        public Account Second { get; set; }
    }
}

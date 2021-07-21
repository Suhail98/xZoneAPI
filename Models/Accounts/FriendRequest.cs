using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace xZoneAPI.Models.Accounts
{
    public class FriendRequest
    {
        [ForeignKey("Sender")]
        public int SenderId { get; set; }
        [ForeignKey("Receiver")]
        public int ReceiverId { get; set; }

        public Account Sender { get; set; }
        public Account Receiver { get; set; }
    }
}

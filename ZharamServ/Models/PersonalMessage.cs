using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZharamServ.Models
{
    public class PersonalMessage
    {
        [Key]
        public Guid MessageId { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string ContentJson { get; set; }
        public DateTime FixedTime { get; set; }

        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZharamServ.Models
{
    public class RoomMessage
    {
        [Key]
        public Guid MessageId { get; set; }
        public Guid UserId { get; set; }
        public Guid RoomId { get; set; }
        public string ContentJson { get; set; }
        public DateTime FixedTime { get; set; }

        public virtual User User { get; set; }
        public virtual Room Room { get; set; }
    }
}
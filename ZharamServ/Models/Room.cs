using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZharamServ.Models
{
    public class Room
    {
        public Guid RoomId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<RoomMessage> RoomMessages { get; set; } 
        public virtual ICollection<User> Users { get; set; }
    }
}
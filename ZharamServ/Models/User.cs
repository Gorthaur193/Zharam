using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZharamServ.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime LastActiveDate { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
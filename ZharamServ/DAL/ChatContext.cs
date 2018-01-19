using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using ZharamServ.Models;

namespace ZharamServ.DAL
{
    public class ChatContext : DbContext
    {
        public ChatContext() : base("ChatContext")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<PersonalMessage> PersonalMessages { get; set; }
        public DbSet<RoomMessage> RoomMessages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<User>()
                .HasMany(c => c.Rooms).WithMany(a => a.Users)
                        .Map(z => z.MapLeftKey("UserId").MapRightKey("RoomId").ToTable("UserRoom"));

        }
    }
}
namespace ZharamServ.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ZharamServ.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ZharamServ.DAL.ChatContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ZharamServ.DAL.ChatContext context)
        {
            User X = new User { Login = "Lesha", PasswordHash = "qwe", Bio = "123", Email = "q45@w.ru", EmailConfirmed = false, Id = Guid.NewGuid(), Name = "Lexa", Phone = "+79991234567", PhoneConfirmed = false, TwoFactorEnabled = false, LastActiveDate = DateTime.Now };
            List<User> users = new List<User>
            {
                X,
                new User{ Login = "Masha", PasswordHash= "qwe", Bio="1234", Email = "q23@w.ru", EmailConfirmed=false, Id = Guid.NewGuid(), Name= "Lexands", Phone="+79991234567", PhoneConfirmed=false, TwoFactorEnabled=false, LastActiveDate = DateTime.Now },
                new User{ Login = "Dasha", PasswordHash= "qwe", Bio="1235", Email = "q3@w.ru", EmailConfirmed=false, Id = Guid.NewGuid(), Name= "Lexaqewrfas", Phone="+79991234567", PhoneConfirmed=false, TwoFactorEnabled=false, LastActiveDate = DateTime.Now },
                new User{ Login = "Gesha", PasswordHash= "qwe", Bio="1236", Email = "q1@w.ru", EmailConfirmed=false, Id = Guid.NewGuid(), Name= "Lexsdfa", Phone="+79991234567", PhoneConfirmed=false, TwoFactorEnabled=false, LastActiveDate = DateTime.Now },
                new User{ Login = "Gonesha", PasswordHash= "qwe", Bio="1237", Email = "q2@w.ru", EmailConfirmed=false, Id = Guid.NewGuid(), Name= "Lexsdfa", Phone="+79991234567", PhoneConfirmed=false, TwoFactorEnabled=false, LastActiveDate = DateTime.Now }
            };

            List<Room> rooms = new List<Room>
            {
                new Room{ RoomId=Guid.NewGuid(), Name="room1", Description="room 1 test room", Admin = X },
                new Room{ RoomId=Guid.NewGuid(), Name="room2", Description="room 2 test room", Admin = X },
                new Room{ RoomId=Guid.NewGuid(), Name="room3", Description="room 3 test room", Admin = X }
            };

            users.ForEach(x => context.Users.AddOrUpdate(x));
            rooms.ForEach(x => context.Rooms.AddOrUpdate(x));
            context.SaveChanges();
        }
    }
}                                                                          
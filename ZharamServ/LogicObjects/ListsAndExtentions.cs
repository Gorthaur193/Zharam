using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ZharamServ.DAL;
using ZharamServ.Logic;

namespace ZharamServ.Logic
{
    public static class CurrentMemo
    {
        public static List<User> UserList { get; set; }
        public static List<Room> RoomList { get; set; }
        public static ChatContext DbContext { get; set; }  
        public static List<(Guid UserId, Guid AuthToken)> AuthList { get; set; }

        static CurrentMemo()
        {
            DbContext = new ChatContext();
            AuthList = new List<(Guid UserId, Guid AuthToken)>();
            RoomList = new List<Room>();
            UserList = new List<User>();
        }
    }
}

namespace ZharamServ
{
    public static class Extentions
    {
        public static User GetById(this List<User> list, Guid id) =>
            list.FirstOrDefault(x => x.UserInDatabase.Id == id);

        public static Room GetById(this List<Room> list, Guid id) =>
            list.FirstOrDefault(x => x.RoomInDatabase.RoomId == id);
        
        public static bool IsExist(this List<User> list, Models.User user)
        {
            bool flag = false;
            foreach (var item in list)
                flag |= item.UserInDatabase.Equals(user);
            return flag;
        }

    }
}
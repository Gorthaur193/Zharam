using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ZharamServ.Logic.CurrentMemo;


namespace ZharamServ.Logic
{
    public class User
    {
        public Models.User UserInDatabase { get; }

        public ModifiedWebSocketHandler UserConnection { get; }
        public Guid CurrentToken { get; }

        public User(ModifiedWebSocketHandler userConnection, Guid userId, Guid token)
        {
            UserInDatabase = DbContext.Users.FirstOrDefault(x => x.Id == userId);
            UserConnection = userConnection;
            CurrentToken = token;

            foreach (var room in UserInDatabase.Rooms)
            {
                var databaseRoom = RoomList.GetById(room.RoomId);
                if (databaseRoom != null)
                    UserList.Add(this);
                else
                {
                    var newRoom = new Room(room.RoomId);
                    RoomList.Add(newRoom);
                    newRoom.UserList.Add(this);
                }    
            }
        }                      
    }       
}
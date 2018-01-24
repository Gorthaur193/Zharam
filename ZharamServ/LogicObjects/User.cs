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
                
        public Room CurrentRoom { get; private set; }
        public void SwitchToRoom(Room room) => CurrentRoom = room;

        public User(ModifiedWebSocketHandler userConnection, Guid userId, Guid token)
        {
            UserInDatabase = DbContext.Users.First(x => x.Id == userId);
            UserConnection = userConnection;
            CurrentToken = token;                                                
        }                      
    }       
}
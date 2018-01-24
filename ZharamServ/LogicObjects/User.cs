using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZharamServ.Logic
{
    public class User
    {
        public Models.User UserInDatabase { get; }

        public ModifiedWebSocketHandler UserConnection { get; }
        public string CurrentToken { get; }
                
        public Room CurrentRoom { get; private set; }
        public void SwitchToRoom(Room room) => CurrentRoom = room;

        public User(ModifiedWebSocketHandler userConnection, Guid userId, Guid token)
        {
                                                
        }                      
    }       
}
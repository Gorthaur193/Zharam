using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZharamServ.Logic
{
    public class User
    {
        public ModifiedWebSocketHandler UserConnection { get; }
        public string Name { get; }
        public string CurrentToken { get; }
        public string Id { get; }
        public Room CurrentRoom { get; private set; }
        public void SwitchToRoom(Room room) => CurrentRoom = room;

        public User(string jsonInfo)
        {
            
        }
    }       
}
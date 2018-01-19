using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.ServiceModel.WebSockets;
using Microsoft.Web.WebSockets;

namespace ZharamServ.Logic
{
    public class Room
    {
        public List<User> UserList { get; private set; }

        public void Broadcast(string message)
        {
            foreach (var item in UserList)
                item.UserConnection.Send(message);
        }

    }
}
using Microsoft.Web.WebSockets;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZharamServ.Logic;
using static ZharamServ.Logic.CurrentMemo;


namespace ZharamServ
{
    public class Ws : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (context.IsWebSocketRequest)
                context.AcceptWebSocketRequest(new ModifiedWebSocketHandler());
        }

        public bool IsReusable => false;
    }

    public class ModifiedWebSocketHandler : WebSocketHandler
    {
        public User Parent { get; private set; }

        public ModifiedWebSocketHandler()
        {
        }

        public override void OnMessage(string message)
        {
        }

        public override void OnOpen()
        {
            var authToken = new Guid(this.WebSocketContext.QueryString["AuthToken"]);
            var login = this.WebSocketContext.QueryString["Login"];
            var userId = DbContext.Users.FirstOrDefault(x => x.Login == login).Id;
            
            if (userId != null && AuthList.Contains((userId, authToken)))
            {
                AuthList.RemoveAll(x => x.UserId == userId);
                var givenToken = Guid.NewGuid();
                Parent = new User(this, userId, givenToken);
                UserList.Add(Parent);
                this.Send(new JObject(new { status = "ok", token = givenToken }).ToString());
            }  
            else
                this.Send(new JObject(new { status = "go away, you dirty cheater!" }).ToString());

        }

        public override void OnClose()
        {
            UserList.Remove(Parent);
            foreach (var room in RoomList)
                room.UserList.Remove(Parent);
        }
    }
}
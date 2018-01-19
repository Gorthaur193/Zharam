using Microsoft.Web.WebSockets;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZharamServ.Logic;

namespace ZharamServ
{
    public class Ws : IHttpHandler
    {
        public static WebSocketCollection Clients { get; private set; }

        public void ProcessRequest(HttpContext context)
        {
            if (context.IsWebSocketRequest)
                context.AcceptWebSocketRequest(new ModifiedWebSocketHandler());
        }

        public bool IsReusable => false;
    }

    public class ModifiedWebSocketHandler : WebSocketHandler
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public User Parent { get; private set; }

        public ModifiedWebSocketHandler()
        {
        }

        public override void OnMessage(string message)
        {
            JObject json = new JObject
            {
                { "Message", message },
                { "Id", Id },
                { "Name", Name }
            };
            Ws.Clients.Broadcast(json.ToString());
        }

        public override void OnOpen()
        {
            this.Name = this.WebSocketContext.QueryString["name"];
            this.Id = Guid.NewGuid().ToString();
            this.Send(Id);
            Ws.Clients.Add(this);
            Ws.Clients.Broadcast(Name + " has connected.");
        }

        public override void OnClose()
        {
            Ws.Clients.Remove(this);
            Ws.Clients.Broadcast($"{Name} has gone away.");
        }
    }
}
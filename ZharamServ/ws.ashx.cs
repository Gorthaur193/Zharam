using Microsoft.Web.WebSockets;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZharamServ
{
    public class Ws : IHttpHandler
    {
        static WebSocketCollection clients = new WebSocketCollection();
        
        public static WebSocketCollection Clients { get { return clients; } set { clients = value; } }

        public void ProcessRequest(HttpContext context)
        {
            if (context.IsWebSocketRequest)
                context.AcceptWebSocketRequest(new MyWebSocketHandler());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class MyWebSocketHandler : WebSocketHandler
    {
        public string Name { get; set; }
        public string Id { get; set; }

        public MyWebSocketHandler()
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
using Microsoft.Web.WebSockets;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZharamServ
{
    /// <summary>
    /// Summary description for ws
    /// </summary>
    public class Ws : IHttpHandler
    {
        public static WebSocketCollection clients = new WebSocketCollection();

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
        public string name;
        public string id;

        public MyWebSocketHandler()
        {
        }

        public override void OnMessage(string message)
        {
            JObject json = new JObject();
            json.Add("Message", message);
            json.Add("Id", id);
            json.Add("Name", name);
            Ws.clients.Broadcast(json.ToString());

        }

        public override void OnOpen()
        {
            this.name = this.WebSocketContext.QueryString["name"];
            this.id = Guid.NewGuid().ToString();
            this.Send(id);
            Ws.clients.Add(this);
            Ws.clients.Broadcast(name + " has connected.");
        }

        public override void OnClose()
        {
            Ws.clients.Remove(this);
            Ws.clients.Broadcast($"{name} has gone away.");
        }
    }

}
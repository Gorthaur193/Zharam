using Microsoft.Web.WebSockets;
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
        private string name;

        public MyWebSocketHandler()
        {
        }

        public override void OnOpen()
        {
            this.name = this.WebSocketContext.QueryString["token"];
            Ws.clients.Add(this);
            Ws.clients.Broadcast(name + " has connected.");
        }

        public override void OnMessage(string message)
        {
            Ws.clients.Broadcast(string.Format("{0} said: {1}", name, message));
        }

        public override void OnClose()
        {
            Ws.clients.Remove(this);
            Ws.clients.Broadcast(string.Format("{0} has gone away.", name));
        }
    }

}
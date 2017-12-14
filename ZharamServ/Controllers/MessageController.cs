using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ZharamServ.Controllers
{
    public class MessageController : ApiController
    {
        public HttpResponseMessage Post(string message, string myid)
        {
            var name = (Ws.Clients.First((A) => (A as MyWebSocketHandler).Id == myid) as MyWebSocketHandler).Name;
            Ws.Clients.Broadcast(new JObject
            {
                { "Message", message },
                { "Id", myid },
                { "Name", name }
            }.ToString());
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
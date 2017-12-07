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
        // POST: api/Message
        public HttpResponseMessage Post(string message, string myid)
        {
            var name = (Ws.clients.First((A) => (A as MyWebSocketHandler).id == myid) as MyWebSocketHandler).name;
            JObject json = new JObject();
            json.Add("Message", message);
            json.Add("Id", myid);
            json.Add("Name", name);
            Ws.clients.Broadcast(json.ToString());
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}

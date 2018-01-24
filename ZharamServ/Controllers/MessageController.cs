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
using ZharamServ.Models;
using static ZharamServ.Logic.CurrentMemo;


namespace ZharamServ.Controllers
{
    public class MessageController : ApiController
    {
        public HttpResponseMessage Post(string json)
        {
            return this.GetType().InvokeMember("PersonalPost", System.Reflection.BindingFlags.InvokeMethod, null, this, new object[] { JObject.Parse(json) }) as HttpResponseMessage;
            //return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public IEnumerable<string> Get(string qwery)
        {                                                                   
            return from A in DbContext.PersonalMessages select A.ContentJson;
        }

        public HttpResponseMessage PersonalPost(JObject json)         // token + message + receiverId
        {
            var personalMessage = new PersonalMessage
            {
                MessageId = Guid.NewGuid(),
                ContentJson = json["Message"].ToString(),
                FixedTime = DateTime.Now,
                SenderId = UserList.FirstOrDefault(x => x.CurrentToken == (Guid)json["Token"]).UserInDatabase.Id,
                ReceiverId = (Guid)json["ReceiverId"]
            };

            UserList.FirstOrDefault(x => x.UserInDatabase.Id == personalMessage.ReceiverId).UserConnection.Send(personalMessage.ContentJson);
            DbContext.PersonalMessages.Add(personalMessage);
            DbContext.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
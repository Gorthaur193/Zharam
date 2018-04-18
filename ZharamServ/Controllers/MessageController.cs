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
        public HttpResponseMessage Post(string json, string messagetype)
        {
            return this.GetType().InvokeMember(messagetype, System.Reflection.BindingFlags.InvokeMethod, null, this, new object[] { JObject.Parse(json) }) as HttpResponseMessage;
            //return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public IEnumerable<string> Get(string qwery)
        {                                                                   
            return from A in DbContext.PersonalMessages select A.ContentJson;
        }

        public HttpResponseMessage PersonalPost(JObject json)         // token + message + receiverId
        {
            try
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
            }
            catch (Exception E)
            {
                return new HttpResponseMessage(HttpStatusCode.Forbidden);
                throw new Exception($"Personal Message gone wrong #{E.Message}#");
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public HttpResponseMessage RoomPost(JObject json)           // token + message + roomid
        {
            try
            {         
                var roomMessage = new RoomMessage
                {
                    MessageId = Guid.NewGuid(),
                    ContentJson = json["Message"].ToString(),
                    FixedTime = DateTime.Now,
                    UserId = UserList.FirstOrDefault(x => x.CurrentToken == (Guid)json["Token"]).UserInDatabase.Id,
                    RoomId = (Guid)json["RoomId"]
                };

                RoomList.GetById(roomMessage.RoomId).Broadcast(roomMessage.ContentJson);
                DbContext.RoomMessages.Add(roomMessage);
            }
            catch (Exception E)
            {
                return new HttpResponseMessage(HttpStatusCode.Forbidden);
                throw new Exception($"Personal Message gone wrong #{E.Message}#");
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
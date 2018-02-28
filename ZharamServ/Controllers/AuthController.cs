using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using ZharamServ;
using ZharamServ.DAL;
using ZharamServ.Logic;
using ZharamServ.Models;
using static ZharamServ.Logic.CurrentMemo;


namespace ZharamServ.Controllers
{
    public class AuthController : ApiController
    {
        // To be changed to secure protocol 
        public JObject Get(string login, string password)
        {
            var user = DbContext.Users.FirstOrDefault(x => x.Login == login && x.PasswordHash == password);
            if (user == null)                                                                                    
                return new JObject(new { status = "fail" });

            if (!UserList.IsExist(user))
            {
                Guid AuthToken = Guid.NewGuid();
                var newUserToBeAuthed = (user.Id, AuthToken);
                AuthList.Add(newUserToBeAuthed);
                return new JObject(new { status = "ok", Login = user.Login, AuthToken });
            }
            else
                return new JObject(new { status = "online" });
        }

        //soon comes registration
        public string Post(string login, string password, string email)
        {
            return "DEPRECATED";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ZharamServ.Controllers
{
    public class LoginController : ApiController
    {
        // GET: api/Login/5
        public string Get(string login, string password)
        {
            return "value";
        }

        public string Post(string login, string password, string email)
        {

            return "";
        }
    }
}

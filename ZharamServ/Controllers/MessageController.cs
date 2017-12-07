using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ZharamServ.Controllers
{
    public class MessageController : ApiController
    {
        // GET: api/Message
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST: api/Message
        public string Post(string value)
        {
            return (value + '1');
        }
    }
}

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zharam.Messaging
{
    public abstract class Message
    {
        Guid Guid { get; }
        public override string ToString()
        {
            string Name = this.GetType().ToString().Substring(17);
            JObject jObject= JObject.FromObject(this);
            jObject.Add("Type", Name);
            return jObject.ToString();
        }
    }

    public class FileMessage : Message
    {
        public Guid Guid { get; private set; }
        public string FileAddress { get; private set; }
        public FileMessage(string json)
        {
            JObject Income = JObject.Parse(json);
            Guid = (Guid)Income["Guid"];
            FileAddress = (string)Income["FileAddress"];
        }
        public FileMessage(string Address, byte a = 0)
        {
            Guid = Guid.NewGuid();
            FileAddress = Address;
        }
    }
    public class TextMessage : Message
    {
        public Guid Guid { get; private set; }
        public string Message { get; private set; }
        public TextMessage(string Address, byte a = 0)
        {
            Guid = Guid.NewGuid();
            Message = Address;
        }

        public TextMessage(string json)
        {
            JObject Income = JObject.Parse(json);
            Guid = (Guid)Income["Guid"];
            Message = (string)Income["Message"];
        }
    }
}

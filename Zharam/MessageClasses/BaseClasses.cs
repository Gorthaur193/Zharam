using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zharam.Messaging
{
    public enum MessageType
    {
        Text,
        File,
    }

    public abstract class Message
    {
        Guid Guid { get; }
        public override string ToString()
        {
            return new JObject(this, new { Type = GetType().ToString() }).ToString();
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
    }
    public class TextMessage : Message
    {
        public Guid Guid { get; private set; }
        public string Message { get; private set; }
        public TextMessage(string json)
        {
            JObject Income = JObject.Parse(json);
            Guid = (Guid)Income["Guid"];
            Message = (string)Income["Message"];
        }
    }
}

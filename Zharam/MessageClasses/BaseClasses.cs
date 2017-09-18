using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zharam.MessageClasses
{
    public enum MessageType
    {
        Text,
        File,
    }

    public interface IMessage
    {
        Guid Guid { get; }
    }

    public class FileMessage : IMessage
    {
        public Guid Guid { get; private set; }
        public string FileAddress { get; set; }
        public FileMessage()
        {
            
        }
    }
    public class TextMessage : IMessage
    {
        public Guid Guid { get; private set; }
        public string Message { get; private set; }
        public TextMessage()
        {

        }
    }
}

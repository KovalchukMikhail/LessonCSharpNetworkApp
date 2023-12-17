using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MessangerGB
{
    public class MessageFactory
    {
        public Message CreateMessage(string text, string nicknameFrom, string nicknameTo, DateTime dateTime)
        {
            return new Message() { Text = text, NicknameFrom = nicknameFrom, NicknameTo = nicknameTo, DateTime = dateTime };
        }
        public static string SerializeMessageToJson(Message message) => JsonSerializer.Serialize(message);

        public static string SerializeMessageToXml(Message message)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Message));
            using(StringWriter stringWriter = new StringWriter())
            {
                using(XmlWriter xmlWriter = XmlWriter.Create(stringWriter))
                {
                    serializer.Serialize(xmlWriter, message);
                    return stringWriter.ToString();
                }
            }
        }
        public static Message DeserializeMessage(string message)
        {
            if (message.StartsWith("<?xml"))
            {
                return DeserializeFromXml(message);
            }
            else
            {
                return DeserializeFromJson(message);
            }
        }
        public static Message? DeserializeFromJson(string message) => JsonSerializer.Deserialize<Message>(message);
        public static Message? DeserializeFromXml(string xmlMessage)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Message));
            using(StringReader stringReader = new StringReader(xmlMessage))
            {
                Message message = serializer.Deserialize(stringReader) as Message;
                return message;
            }
            
        }
    }
}

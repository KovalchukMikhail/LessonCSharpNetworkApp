using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MessangerGB
{
    public class Infrastructure
    {
        public void SendJsonMessage(Message message, UdpClient udpClient, IPEndPoint iPEndPoint)
        {
            string json = MessageFactory.SerializeMessageToJson(message);
            SendMessage(udpClient, iPEndPoint, json);
        }
        public void SendXmlMessage(Message message, UdpClient udpClient, IPEndPoint iPEndPoint)
        {
            string xml = MessageFactory.SerializeMessageToXml(message);
            SendMessage(udpClient, iPEndPoint, xml);
        }
        public void SendMessage(UdpClient udpClient, IPEndPoint iPEndPoint, string msg)
        {
            byte[] data = Encoding.UTF8.GetBytes(msg);
            udpClient.Send(data, data.Length, iPEndPoint);
        }
        public Message GetMessage(UdpClient udpClient, ref IPEndPoint iPEndPoint)
        {
            byte[] buffer = udpClient.Receive(ref iPEndPoint);

            var messageText = Encoding.UTF8.GetString(buffer);

            return MessageFactory.DeserializeMessage(messageText);
        }
    }
}

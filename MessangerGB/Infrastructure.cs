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
        public void SendMessage(Message message, UdpClient udpClient, IPEndPoint iPEndPoint)
        {
            string json = message.SerializeMessageToJson();
            byte[] data = Encoding.UTF8.GetBytes(json);
            udpClient.Send(data, data.Length, iPEndPoint);
        }
        public Message GetMessage(UdpClient udpClient, IPEndPoint iPEndPoint)
        {
            byte[] buffer = udpClient.Receive(ref iPEndPoint);

            var messageText = Encoding.UTF8.GetString(buffer);

            return Message.DeserializeFromJson(messageText);
        }
    }
}

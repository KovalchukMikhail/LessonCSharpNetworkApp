using System.Net.Sockets;
using System.Net;
using System.Text;
using MessangerGB;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SentMessage(args[0], args[1]);
        }

        public static void SentMessage(string from, string ip)
        {

            UdpClient udpClient = new UdpClient(12346);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), 12345);
            Infrastructure infrastructure = new Infrastructure();
            MessageFactory factory = new MessageFactory();

            while (true)
            {
                string messageText;
                do
                {
                    Console.WriteLine("Введите сообщение.");
                    messageText = Console.ReadLine();
                }
                while (string.IsNullOrEmpty(messageText));
                
                Message message = factory.CreateMessage(messageText, from, "Server", DateTime.Now);
                infrastructure.SendJsonMessage(message, udpClient, iPEndPoint);
                if (messageText.ToLower() == "exit")
                {
                    Console.WriteLine("Досвидание!");
                    return;
                }
                    
                Message answer = infrastructure.GetMessage(udpClient, ref iPEndPoint);
                //answer.Print();

            }

        }
    }
}
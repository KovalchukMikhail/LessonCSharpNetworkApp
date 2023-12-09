using System.Net.Sockets;
using System.Net;
using System.Text;

namespace MessangerGB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Server("Hello");
        }
        public static void Server(string name)
        {
            UdpClient udpClient = new UdpClient(12345);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12346);
            Infrastructure infrastructure = new Infrastructure();

            Console.WriteLine("Сервер ждет сообщение от клиента");

            while (true)
            {
                Message message = infrastructure.GetMessage(udpClient, iPEndPoint);
                message.Print();

                Message answer = new Message() { Text = "The message has been delivered", NicknameFrom = "Server", NicknameTo = message.NicknameFrom, DateTime = DateTime.Now };
                infrastructure.SendMessage(answer, udpClient, iPEndPoint);

            }
        }


    }
}
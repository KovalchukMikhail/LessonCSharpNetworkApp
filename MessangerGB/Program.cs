using System.Net.Sockets;
using System.Net;
using System.Text;

namespace MessangerGB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var task = Task.Run(Server);

            task.Wait();

        }
        public static void Server()
        {
            UdpClient udpClient = new UdpClient(12345);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0);
            Infrastructure infrastructure = new Infrastructure();
            using CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;

            Console.WriteLine("Сервер ждет сообщение от клиента");
            MessageFactory factory = new MessageFactory();

            while (true)
            {
                Message message = infrastructure.GetMessage(udpClient, ref iPEndPoint);
                if(message.Text.ToLower() == "exit")
                {
                    cancelTokenSource.Cancel();
                    return;
                }
                message.Print();
                
                Task.Run(() =>
                {
                    if (token.IsCancellationRequested)
                        token.ThrowIfCancellationRequested();

                    Message answer = factory.CreateMessage("The message has been delivered", "Server", message.NicknameFrom, DateTime.Now);
                    infrastructure.SendJsonMessage(answer, udpClient, iPEndPoint);
                    }, token);

            }

        }


    }
}
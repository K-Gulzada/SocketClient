using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            SendMessageFromSocket(5001);
        }

        static void SendMessageFromSocket(int port)
        {           

            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = ipHost.AddressList[0];
            //IPAddress ipAddress = IPAddress.Parse("192.168.110.213");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);

            Socket sendler = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            sendler.Connect(ipEndPoint);
            Console.WriteLine("Введите смс");
            string msg = Console.ReadLine();

            byte[] sendMsg = Encoding.UTF8.GetBytes(msg);
            int bytesSent = sendler.Send(sendMsg);
            byte[] bytes = new byte[1024];
            int byteReceive = sendler.Receive(bytes);

            Console.WriteLine("ответ сервера: " + Encoding.UTF8.GetString(bytes));

            if (msg.IndexOf("<eof>") == -1)
            {
                SendMessageFromSocket(port);
            }

            sendler.Shutdown(SocketShutdown.Both);
            sendler.Close();

        }
    }
}

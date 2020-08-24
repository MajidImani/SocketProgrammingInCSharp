using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TcpSocketClient client = new TcpSocketClient("127.0.0.1", 6009);
            client.StartMessaging("My name is Majid!");
        }
    }
}

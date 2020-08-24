using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TcpSocketServer ss = new TcpSocketServer(6009);
            ss.StartMessaging("Your Message Received");
        }
    }
}

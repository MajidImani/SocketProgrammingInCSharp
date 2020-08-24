using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class TcpSocketServer : ITcpSocketServer
    {
        public int Port { get; set; }
        public TcpSocketServer(int port)
        {
            this.Port = port;
        }
        public void ReceiveAndWriteMessage(TcpClient client, NetworkStream stream, string message)
        {
            StreamWriter sw = new StreamWriter(client.GetStream());
            try
            {
                byte[] buffer = new byte[1024];
                stream.Read(buffer, 0, buffer.Length);
                int recv = 0;
                foreach (byte b in buffer)
                {
                    if (b != 0)
                    {
                        recv++;
                    }
                }
                string request = Encoding.UTF8.GetString(buffer, 0, recv);
                Console.WriteLine($"request received: {request}");
                sw.WriteLine("Your message recieved");
                sw.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception");
                sw.WriteLine(ex.ToString());
            }
        }

        public NetworkStream CreateNetworkStream(TcpClient client)
        {
            return client.GetStream();
        }

        public TcpClient WaitForConnection(TcpListener listener)
        {
            Console.WriteLine("Waiting for a connection.");
            return listener.AcceptTcpClient();
        }

        public TcpListener CreateListener(int port)
        {
            return new TcpListener(System.Net.IPAddress.Any, port);
        }
        public void StartMessaging(string message)
        {
            TcpListener listener = CreateListener(this.Port);
            listener.Start();
            while (true)
            {
                TcpClient client = WaitForConnection(listener);
                Console.WriteLine("Client Accepted");
                NetworkStream stream = CreateNetworkStream(client);
                ReceiveAndWriteMessage(client, stream, message);
            }
        }

    }
}

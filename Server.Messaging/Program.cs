using ChatRoom.Contracts.Model;
using ChatRoom.Services.Service;
using Server.Services.Service;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Server.Messaging
{
    public class Program
    {
        private static Room Room = new Room("Public_ChatRoom");
        public static void Main(string[] args)
        {
            TcpSocketServer ss = new TcpSocketServer(new ChatRoomService(), 6009);
            TcpListener tcpListener = ss.CreateListener(6009);
            tcpListener.Start();
            while (true)
            {
                TcpClient client = ss.WaitForConnection(tcpListener);
                Task.Run(() => ProcessMessages(ss, client));
            }
        }

        private static void ProcessMessages(TcpSocketServer ss, TcpClient client)
        {
            while (true)
            {
                Console.WriteLine("Client Accepted");
                try
                {
                    if (client == null || !client.Connected)
                    {
                        break;
                    }
                    NetworkStream stream = ss.CreateNetworkStream(client);
                    ss.ReceiveAndWriteMessage(client, stream, Room);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
        }
    }
}

using ChatRoom.Contracts.Model;
using ChatRoom.Services.Service;
using Server.Contracts.Service;
using Server.Services.Service;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Server.Messaging
{
    public class Program
    {
        public const int Port = 6009;
        public const string RoomName = "Public_ChatRoom";

        private static Room Room = new Room(RoomName);
        public static void Main(string[] args)
        {
            Start();
        }

        private static void Start()
        {
            try
            {
                ITcpSocketServer tcpSocketServer = new TcpSocketServer(new ChatRoomService(), Port);
                TcpListener tcpListener = tcpSocketServer.StartListening();
                while (true)
                {
                    TcpClient client = tcpSocketServer.WaitForConnection(tcpListener);
                    Task.Run(() => Process(tcpSocketServer, client));
                }
            }
            catch (Exception ex)
            {
                PrintException(ex);
                Console.ReadKey();
            }
        }

        private static void Process(ITcpSocketServer tcpSocketServer, TcpClient client)
        {
            while (true)
            {
                Console.WriteLine("Client Accepted");
                if (!IsClientConnected(client))
                    break;

                NetworkStream stream = tcpSocketServer.CreateNetworkStream(client);
                tcpSocketServer.ProcessRequest(client, stream, Room);
            }
        }

        private static bool IsClientConnected(TcpClient client)
        {
            return client != null && client.Connected;
        }

        private static void PrintException(Exception ex)
        {
            if (ex != null)
            {
                Console.WriteLine($"Exception->{ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"InnerException-> {ex.InnerException.Message}");
                }
            }
        }
    }
}

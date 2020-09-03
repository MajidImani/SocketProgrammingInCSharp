using ChatRoom.Contracts.Model;
using ChatRoom.Contracts.Service;
using ChatRoom.Services.Service;
using Server.Contracts.Service;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Server.Services.Service
{
    public class TcpSocketServer : ITcpSocketServer
    {
        private readonly IChatRoomService _chatRoomService;
        public int Port { get; set; }

        public TcpSocketServer(int port)
        {
            this.Port = port;
        }

        public TcpSocketServer(IChatRoomService chatRoomService, int port)
        {
            this.Port = port;
            _chatRoomService = chatRoomService;
        }

        public void ProcessRequest(TcpClient client, NetworkStream stream, Room room)
        {
            try
            {
                string request = ParseStreamAndGetRequest(stream);
                Console.WriteLine($"request received: {request}");
                string[] splittedMessage = GetSplittedMessageByRequest(request);
                string sender = GetSenderByRequest(request, splittedMessage);

                if (IsJoinOrLeftRequest(request))
                {
                    string response = "";
                    if (IsJoinRequest(request))
                    {
                        response = GetJoinResponse(client, room, sender);
                        _chatRoomService.SendMessageToClient(client, response);
                    }
                    else
                    {
                        response = GetLeftResponse(sender);
                        _chatRoomService.LeftTheRoom(client, sender, room);
                    }
                    _chatRoomService.BroadcastMessage(sender, room.Name, response);
                }
                else
                {
                    var message = splittedMessage[0].Split(':')[1];
                    var receiver = splittedMessage[2].Split(':')[1];
                    if (string.IsNullOrWhiteSpace(receiver))
                    {
                        _chatRoomService.BroadcastMessage(sender, room.Name, FormatMessageForSend(sender, message));
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.ToString()}");
                _chatRoomService.SendMessageToClient(client, $"Exception: {ex.ToString()}");
            }
        }


        private static bool IsJoinRequest(string request)
        {
            return request.Contains("join:");
        }

        private static string ParseStreamAndGetRequest(NetworkStream stream)
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
            return request;
        }
        public NetworkStream CreateNetworkStream(TcpClient client)
        {
            return client.GetStream();
        }

        public TcpClient WaitForConnection(TcpListener listener)
        {
            Console.WriteLine("Wait for connection...");
            return listener.AcceptTcpClient();
        }

        public TcpListener StartListening()
        {
            TcpListener tcpListener = null;
            tcpListener = new TcpListener(System.Net.IPAddress.Any, this.Port);
            tcpListener.Start();
            return tcpListener;
        }
        private static string FormatMessageForSend(string sender, string message)
        {
            return $"{sender}: {message}";
        }

        private static string GetLeftResponse(string sender)
        {
            return $"{sender} left the room";
        }

        private string GetJoinResponse(TcpClient client, Room room, string sender)
        {
            string response;
            if (!_chatRoomService.IsDuplicateUser(sender))
            {
                _chatRoomService.JoinToRoom(client, sender, room);
                response = $"{sender} join to room";
            }
            else
            {
                response = $"DuplicateUser";
            }

            return response;
        }

        private static string GetSenderByRequest(string request, string[] splittedMessage)
        {
            if (IsJoinOrLeftRequest(request))
            {
                return splittedMessage[1];
            }
            return splittedMessage[1].Split(':')[1];
        }

        private string[] GetSplittedMessageByRequest(string request)
        {
            if (IsJoinOrLeftRequest(request))
            {
                return GetSplittedMessage(request, ':');
            }
            return GetSplittedMessage(request, '|');
        }

        private static bool IsJoinOrLeftRequest(string request)
        {
            return request.Contains("left:") || request.Contains("join:");
        }

        private static string[] GetSplittedMessage(string request, char splitCharacter)
        {
            return request.Split(splitCharacter);
        }

        public void FlushStream(StreamWriter sw)
        {
            sw.Flush();
        }

    }
}

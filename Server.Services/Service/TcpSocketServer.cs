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
    //پیاده سازی اینترفیس های لازم برای سرور
    public class TcpSocketServer : ITcpSocketServer
    {
        private readonly IChatRoomService _chatRoomService;
        public int Port { get; set; }
        public TcpSocketServer(IChatRoomService chatRoomService, int port)
        {
            this.Port = port;
            _chatRoomService = chatRoomService;
        }

        //اصلی ترین متد سمت سرور
        public void ReceiveAndWriteMessage(TcpClient client, NetworkStream stream, Room room)
        {
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
                TcpClient recClient = null;
                string response = "";
                string sender = "";

                //اگر پیام از نوع ملحق شدن بود
                //در اینجا یوزر جوین می شود
                if (request.Contains("join:"))
                {
                    var splittedMessage = request.Split(':');
                    sender = splittedMessage[1];
                    if (!_chatRoomService.IsDuplicateUser(sender))
                    {
                        _chatRoomService.JoinToRoom(client, sender, room);
                        response = $"{sender} join to room";
                    }
                    else
                    {
                        response = $"DuplicateUser";
                    }
                    _chatRoomService.SendMessageToClient(client, response);
                    _chatRoomService.BroadcastMessage(sender, "Public_ChatRoom", response);
                }
                //در اینجا یوزر لفت داده است
                else if (request.Contains("left:"))
                {
                    var splittedMessage = request.Split(':');
                    sender = splittedMessage[1];
                    response = $"{sender} left the room";
                    _chatRoomService.LeftTheRoom(client, sender, room);
                    _chatRoomService.BroadcastMessage(sender, "Public_ChatRoom", response);
                }
                //در اینجا یوزر ارسال پیام انجام داده است
                else
                {
                    var splittedMessage = request.Split('|');
                    var message = splittedMessage[0].Split(':')[1];
                    sender = splittedMessage[1].Split(':')[1];
                    var receiver = splittedMessage[2].Split(':')[1];
                    response = $"{sender}: {message}";
                    if (string.IsNullOrWhiteSpace(receiver))
                    {
                        _chatRoomService.BroadcastMessage(sender, "Public_ChatRoom", response);
                    }
                }

            }
            catch (Exception ex)
            {
                _chatRoomService.SendMessageToClient(client, $"Exception: {ex.ToString()}");
            }
        }

        public void FlushStream(StreamWriter sw)
        {
            sw.Flush();
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
      
        }
}

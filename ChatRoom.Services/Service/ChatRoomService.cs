using ChatRoom.Contracts.Model;
using ChatRoom.Contracts.Service;
using System.Net.Sockets;
using System;
using System.IO;
using System.Linq;

namespace ChatRoom.Services.Service
{
    public class ChatRoomService : IChatRoomService
    {
        public void JoinToRoom(TcpClient client, string username, Room room)
        {
            Chat.AddNewRoom(room);
            Chat.AddUserToRoom(username);
            Chat.AddTcpClient(client);
            Chat.AddUserRoomPair(username, room.Name);
            Chat.AddUserSocketPair(username, client);
        }

        public void BroadcastMessage(string sender, string roomName, string message)
        {
            var usersExceptSender = Chat.RoomUsers.Where(a => !a.Equals(sender));

            foreach (var username in usersExceptSender)
            {
                SendMessageToClient(Chat.UserSockets[username], message);
            }
        }
        
        public bool IsDuplicateUser(string username)
        {
            return Chat.IsDuplicateUser(username);
        }
        
        public void SendMessageToClient(TcpClient client, string message)
        {
            StreamWriter sw = new StreamWriter(client.GetStream());
            sw.WriteLine(message);
            sw.Flush();
        }

        public void LeftTheRoom(TcpClient client, string username, Room room)
        {
            Chat.RoomUsers.Remove(username);
            Chat.TcpSocketClients.Remove(client);
            Chat.UserRooms.Remove(username);
            Chat.UserSockets.Remove(username);
            client.Close();
        }
    }
}

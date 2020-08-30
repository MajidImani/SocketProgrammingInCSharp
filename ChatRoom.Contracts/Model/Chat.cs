using Client.Contracts.Service;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System;

namespace ChatRoom.Contracts.Model
{
    public static class Chat
    {
        public static List<string> RoomUsers { get; set; } = new List<string>();
        public static List<TcpClient> TcpSocketClients { get; set; } = new List<TcpClient>();
        
        public static List<Room> Rooms { get; set; } = new List<Model.Room>();
        
        public static Dictionary<string, string> UserRooms { get; set; } = new Dictionary<string, string>();

        public static void AddUserSocketPair(string username, TcpClient client)
        {
            UserSockets.Add(username, client);
        }

        public static Dictionary<string, TcpClient> UserSockets { get; set; } = new Dictionary<string, TcpClient>();
        
        public static bool IsDuplicateUser(string username)
        {
            var Username = RoomUsers.FirstOrDefault(a => a.Equals(username));
            if (string.IsNullOrEmpty(Username))
            {
                return false;
            }
            return true;
        }

        public static void AddNewRoom(Room room)
        {
            if (!IsRoomExist(room))
            {
                Rooms.Add(room);
            }
        }

        private static bool IsRoomExist(Room room)
        {
            return Rooms.Any(r => r.Name.Equals(room.Name));
        }

        public static void AddUserToRoom(string username)
        {
            RoomUsers.Add(username);
        }

        public static void AddTcpClient(TcpClient client)
        {
            TcpSocketClients.Add(client);
        }
        public static void AddUserRoomPair(string username, string name)
        {
            UserRooms.Add(username, name);
        }

    }
}

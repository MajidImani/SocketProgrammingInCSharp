using Client.Contracts.Service;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace ChatRoom.Contracts.Model
{
    public static class Chat
    {
        public static List<string> RoomUsers { get; set; } = new List<string>();
        public static List<TcpClient> TcpSocketClients { get; set; } = new List<TcpClient>();
        public static List<Room> Rooms { get; set; } = new List<Model.Room>();
        public static Dictionary<string, string> UserRooms { get; set; } = new Dictionary<string, string>();
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
    }
}

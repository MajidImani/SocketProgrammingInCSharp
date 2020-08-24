using ChatRoom.Contracts.Model;
using Client.Contracts.Service;
using System.Net.Sockets;

namespace ChatRoom.Contracts.Service
{
    public interface IChatRoomService
    {
        void JoinToRoom(TcpClient client,string username, Room room);
        void BroadcastMessage(string sender, string roomName, string message);
        bool IsDuplicateUser(string username);
        void SendMessageToClient(TcpClient client, string message);
        void LeftTheRoom(TcpClient client, string sender, Room room);
    }
}

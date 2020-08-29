using ChatRoom.Contracts.Model;
using System.Net.Sockets;

namespace Server.Contracts.Service
{
    public interface ITcpSocketServer
    {
        TcpListener StartListening();
        TcpClient WaitForConnection(TcpListener listener);
        NetworkStream CreateNetworkStream(TcpClient client);
        void ProcessRequest(TcpClient client, NetworkStream stream, Room room);
    }

}

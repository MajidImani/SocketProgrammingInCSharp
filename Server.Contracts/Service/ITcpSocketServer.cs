using ChatRoom.Contracts.Model;
using System.Net.Sockets;

namespace Server.Contracts.Service
{
    public interface ITcpSocketServer
    {
        TcpListener CreateListener(int port);
        TcpClient WaitForConnection(TcpListener listener);
        NetworkStream CreateNetworkStream(TcpClient client);
        void ReceiveAndWriteMessage(TcpClient client, NetworkStream stream, Room room);
    }

}

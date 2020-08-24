using System.Net.Sockets;

namespace Server
{
    public interface ITcpSocketServer
    {
        TcpListener CreateListener(int port);
        TcpClient WaitForConnection(TcpListener listener);
        NetworkStream CreateNetworkStream(TcpClient client);
        void ReceiveAndWriteMessage(TcpClient client, NetworkStream stream, string message);
    }
}

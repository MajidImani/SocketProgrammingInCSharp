using System.Net.Sockets;

namespace Client
{
    public interface ITcpSocketClient
    {
        TcpClient CreateTcpConnection(string ip, int port);
        NetworkStream CreateNetworkStream(TcpClient client);
        void SendMessage(NetworkStream stream, string message);
        void ReceiveMessage(NetworkStream stream);
        void Close(NetworkStream stream, TcpClient client);
        void StartMessaging(string message);
    }
}

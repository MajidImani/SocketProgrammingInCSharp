using System.Net.Sockets;

namespace Client.Contracts.Service
{
    public interface ITcpSocketClient
    {
        TcpClient CreateTcpConnection(string ip, int port);
        NetworkStream CreateNetworkStream(TcpClient client);
        void SendMessage(NetworkStream stream, string message);
        void WriteReceiveMessageToConsole(NetworkStream stream);
        void Close(NetworkStream stream, TcpClient client);
    }
}

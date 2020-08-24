using ChatRoom.Contracts.Model;
using Client.Contracts.Service;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Client.Services.Service
{

    public class TcpSocketClient : ITcpSocketClient
    {
        public User User { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }
        public TcpSocketClient(User user, string ip, int port)
        {
            this.Ip = ip;
            this.Port = port;
            this.User = user;
        }
        
        public TcpClient CreateTcpConnection(string ip, int port)
        {
            return new TcpClient(ip, port);
        }

        public NetworkStream CreateNetworkStream(TcpClient client)
        {
            return client.GetStream();
        }

        public void SendMessage(NetworkStream stream, string message)
        {
            byte[] sendData = ConvertMessageToByteArray(message);
            SendByteArray(sendData, stream);
        }

        public string ReceivedMessage(NetworkStream stream)
        {
            StreamReader sr = new StreamReader(stream);
            string response = sr.ReadLine();
            return response;
        }

        public void WriteReceiveMessageToConsole(NetworkStream stream)
        {
            string recMessage = ReceivedMessage(stream);
            Console.WriteLine(recMessage);
        }

        public void Close(NetworkStream stream, TcpClient client)
        {
            stream.Close();
            client.Close();
        }

        private void SendByteArray(byte[] sendData, NetworkStream stream)
        {
            stream.Write(sendData, 0, sendData.Length);
            Console.WriteLine("Sending data to server...");
        }

        private byte[] ConvertMessageToByteArray(string messageToSend)
        {
            int byteCount = Encoding.ASCII.GetByteCount(messageToSend + 1);
            byte[] sendData = new byte[byteCount];
            sendData = Encoding.ASCII.GetBytes(messageToSend);
            return sendData;
        }
        
        
    }
}

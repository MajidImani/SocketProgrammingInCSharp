using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public class TcpSocketClient : ITcpSocketClient
    {
        public string Ip { get; set; }
        public int Port { get; set; }
        public TcpSocketClient(string ip, int port)
        {
            this.Ip = ip;
            this.Port = port;
        }
        public void StartMessaging(string message)
        {
            while (true)
            {
                try
                {
                    TcpClient client = CreateTcpConnection(this.Ip, this.Port);
                    NetworkStream stream = CreateNetworkStream(client);
                    SendMessage(stream, message);
                    ReceiveMessage(stream);
                    Close(stream, client);
                    Console.ReadKey();
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to Connect...");
                }
            }
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

        public void ReceiveMessage(NetworkStream stream)
        {
            StreamReader sr = new StreamReader(stream);
            string response = sr.ReadLine();
            Console.WriteLine(response);
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

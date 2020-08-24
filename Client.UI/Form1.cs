using ChatRoom.Contracts.Factory;
using Client.Services.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public TcpSocketClient Client { get; set; }
        public TcpClient TcpClient { get; set; }
        public NetworkStream NetworkStream { get; set; }
        private void JoinBtn_Click(object sender, EventArgs e)
        {
            var username = UsernameTxtBox.Text;
            if (ValidateUsername(username))
            {
                JoinBtn.Enabled = false;
                var user = UserFactory.CreateUser(username);
                Client = new TcpSocketClient(user, "127.0.0.1", 6009);
                TcpClient = Client.CreateTcpConnection(Client.Ip, Client.Port);
                NetworkStream = Client.CreateNetworkStream(TcpClient);
                Client.SendMessage(NetworkStream, $"join:{user.Username}");
                var response = Client.ReceivedMessage(NetworkStream);
                if (response.Equals("DuplicateUser"))
                {
                    JoinBtn.Enabled = true;
                    MessageBox.Show("Duplicated Username Error");
                }
                else
                {
                    ChatMessageRichTxtBox.Text += response;
                    ChatMessageRichTxtBox.Text += "\n";
                    ShowBroadCastMessages(NetworkStream);
                    button1.Enabled = true;
                }
            }
        }

        private void ShowBroadCastMessages(NetworkStream networkStream)
        {
            Task.Run(() => ShowMessage(networkStream));
        }

        private void ShowMessage(NetworkStream networkStream)
        {
            while (true)
            {
                var message = Client.ReceivedMessage(networkStream);
                if (!string.IsNullOrWhiteSpace(message))
                {
                    ChatMessageRichTxtBox.Text += message;
                    ChatMessageRichTxtBox.Text += "\n";
                }
            }
        }

        private static bool ValidateUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Please Enter Your Username");
                return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NetworkStream = Client.CreateNetworkStream(TcpClient);
            var message = MessgaeRichTextBox.Text;
            if (!string.IsNullOrWhiteSpace(message))
            {
                ChatMessageRichTxtBox.Text += $"You: {message}";
                ChatMessageRichTxtBox.Text += "\n";
                Client.SendMessage(NetworkStream, $"message:{message}|sender:{Client.User.Username}|receiver:{null}");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var window = MessageBox.Show(
                "Left The Room?",
                "Are you sure?",
                MessageBoxButtons.YesNo);

            if (window == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                NetworkStream = Client.CreateNetworkStream(TcpClient);
                Client.SendMessage(NetworkStream, $"left:{Client.User.Username}");
            }
        }
    }
}

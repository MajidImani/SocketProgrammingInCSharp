using Server.Contracts.Service;
using Server.Services.Service;
using System;
using Xunit;

namespace ServerSocket.Test
{
    public class TcpSocketServiceTests
    {
        ITcpSocketServer _tcpSocketServer;

        [Theory]
        [InlineData(6009)]
        [InlineData(80)]
        [InlineData(7000)]
        [InlineData(8000)]
        [InlineData(9000)]
        public void Start_Listening_Should_Start_New_Listener_When_Port_Is_Valid(int port)
        {
            _tcpSocketServer = new TcpSocketServer(port);
            var tcpListener = _tcpSocketServer.StartListening();
            Assert.True(tcpListener.Server.IsBound);
            tcpListener.Stop();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(90000)]
        public void Start_Listening_Should_Throw_Exception_When_Port_Is_InValid(int port)
        {
            _tcpSocketServer = new TcpSocketServer(port);
            Assert.Throws<ArgumentOutOfRangeException>(() => _tcpSocketServer.StartListening());
        }
    }
}

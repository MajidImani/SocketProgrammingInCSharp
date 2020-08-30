using FluentAssertions;
using Server.Contracts.Service;
using Server.Services.Service;
using System;
using System.Net.Sockets;
using Xunit;

namespace ServerSocket.Test
{
    public class TcpSocketServiceTests
    {
        private ITcpSocketServer _tcpSocketServer;


        [Theory]
        [InlineData(6009)]
        [InlineData(80)]
        [InlineData(7000)]
        [InlineData(8000)]
        [InlineData(9000)]
        public void Start_Listening_Should_Start_New_Listener_When_Port_Is_Valid(int port)
        {
            _tcpSocketServer = new TcpSocketServer(port);
            TcpListener tcpListener = _tcpSocketServer.StartListening();
            tcpListener.Server.IsBound.Should().Be(true);
            tcpListener.Stop();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(90000)]
        public void Start_Listening_Should_Throw_Exception_When_Port_Is_InValid(int port)
        {
            _tcpSocketServer = new TcpSocketServer(port);
            _tcpSocketServer.Invoking(a => a.StartListening()).Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}

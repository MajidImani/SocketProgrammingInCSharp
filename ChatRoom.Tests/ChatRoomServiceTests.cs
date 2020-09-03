using ChatRoom.Contracts.Model;
using ChatRoom.Contracts.Service;
using ChatRoom.Services.Service;
using FluentAssertions;
using System.Collections.Generic;
using System.Net.Sockets;
using Xunit;

namespace ChatRoom.Tests
{
    public class ChatRoomServiceTests
    {
        private readonly IChatRoomService _chatRoomService;
        public ChatRoomServiceTests()
        {
            _chatRoomService = new ChatRoomService();
        }


        [Theory]
        [InlineData("aUser", "Public_ChatRoom")]
        [InlineData("bUser", "Public_ChatRoom")]
        [InlineData("cUser", "Public_ChatRoom")]
        public void Join_To_Room_Join_New_User_And_Connections_To_Room(string username, string roomName)
        {
            Chat.Rooms = new List<Room>();
            TcpClient tcpClient = new TcpClient();
            var room = new Room(roomName);
            _chatRoomService.JoinToRoom(tcpClient, username, room);
            Chat.Rooms.Should().Contain(room);
            Chat.RoomUsers.Should().Contain(username);
            Chat.TcpSocketClients.Should().Contain(tcpClient);
            Chat.UserRooms.Should().Contain(new KeyValuePair<string, string>(username, room.Name));
            Chat.UserSockets.Should().Contain(new KeyValuePair<string, TcpClient>(username, tcpClient));
            tcpClient.Client.Should().NotBe(null);
            ClearTestData();
        }

        [Theory]
        [InlineData("aUser")]
        public void Is_Duplicate_User_Validate_User_Duplication_If_New_User_Is_Exist(string username)
        {
            Chat.RoomUsers = new List<string>();
            _chatRoomService.IsDuplicateUser(username).Should().Be(false);
            Chat.RoomUsers.Add(username);
            _chatRoomService.IsDuplicateUser(username).Should().Be(true);
            ClearTestData();
        }

        [Theory]
        [InlineData("aUser", "Public_ChatRoom")]
        public void Left_The_Room_Should_Left_User_And_Connections_From_Room(string username, string roomName)
        {
            Chat.Rooms = new List<Room>();
            TcpClient tcpClient = new TcpClient();
            var room = new Room(roomName);
            _chatRoomService.JoinToRoom(tcpClient, username, room);
            _chatRoomService.LeftTheRoom(tcpClient, username, room);
            Chat.RoomUsers.Should().NotContain(username);
            Chat.TcpSocketClients.Should().NotContain(tcpClient);
            Chat.UserRooms.Should().NotContain(new KeyValuePair<string, string>(username, room.Name));
            Chat.UserSockets.Should().NotContain(new KeyValuePair<string, TcpClient>(username, tcpClient));
            tcpClient.Client.Should().Be(null);
            ClearTestData();
        }

        private static void ClearTestData()
        {
            Chat.Rooms.Clear();
            Chat.RoomUsers.Clear();
            Chat.UserRooms.Clear();
            Chat.UserSockets.Clear();
            Chat.TcpSocketClients.Clear();
        }
    }
}

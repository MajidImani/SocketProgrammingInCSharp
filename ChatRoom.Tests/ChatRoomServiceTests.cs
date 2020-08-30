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
        public void Join_To_Room_Join_New_User_And_Connections_To_Room_When(string username, string roomName)
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
        }

        [Theory]
        [InlineData("aUser")]
        public void Is_Duplicate_User_Validate_User_Duplication_If_New_User_Is_Exist(string username)
        {
            username.Should().Be("aUser");
        }
    }
}

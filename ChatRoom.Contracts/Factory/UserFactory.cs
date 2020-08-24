using ChatRoom.Contracts.Model;

namespace ChatRoom.Contracts.Factory
{
    public class UserFactory
    {
        public static User CreateUser(string username)
        {
            return new User(username);
        }
    }
}

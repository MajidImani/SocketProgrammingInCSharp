namespace ChatRoom.Contracts.Model
{
    public class User
    {
        public User(string username)
        {
            this.Username = username;
        }

        public string Username { get; set; }
    }
}

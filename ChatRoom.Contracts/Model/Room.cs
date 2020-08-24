namespace ChatRoom.Contracts.Model
{    
    public class Room
    {        
        public Room(string roomName)
        {
            this.Name = roomName;
        }

        public string Name { get; set; }
    }
}

using GlobalService.Utilities;

namespace GlobalService.DTO
{
    public class ChatDTO
    {
        public Guid ChatId{ get; set; }
        public string ChatName { get; set; }
        public ChatType ChatType { get; set; }
        public string ChatTypeDescription => ChatType.ToString();
    }
}

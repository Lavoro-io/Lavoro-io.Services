namespace GlobalService.DTO
{
    public class ChatDTO
    {
        public Guid ChatCode { get; set; }
        public string ChatName { get; set; }
        public List<UserDTO> Users { get; set; }
    }
}

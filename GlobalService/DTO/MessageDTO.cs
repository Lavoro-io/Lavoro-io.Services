namespace GlobalService.DTO
{
    public class MessageDTO
    {
        public Guid MessageId { get; set; }
        public Guid UserId { get; set; }
        public string Message { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalService.DAL
{
    [Table("Messages")]
    public class MessageDAL : BaseDAL
    {
        [Key]
        public Guid MessageId { get; set; }

        [MaxLength(255)]
        public string Message { get; set; }

        public Guid ChatId { get; set; }
        public ChatDAL Chat { get; set; }

        public Guid UserId { get; set; }
        public UserDAL User { get; set; }
    }
}

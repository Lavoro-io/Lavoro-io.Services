using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalService.DAL
{
    [Table("Chats")]
    public class ChatDAL : BaseDAL
    {
        [Key]
        public Guid ChatId { get; set; }
    }
}

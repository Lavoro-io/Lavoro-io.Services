using GlobalService.Utilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalService.DAL
{
    [Table("Chats")]
    public class ChatDAL : BaseDAL
    {
        [Key]
        public Guid ChatId { get; set; }

        public string ChatName { get; set; }

        public ChatType ChatType { get; set; }

        public bool IsActive { get; set; }
    }

}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalService.DAL
{
    [Table("ChatUsers")]
    public class ChatUsersDAL : BaseDAL
    {
        [Key]
        public Guid ChatUsersId { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public UserDAL User { get; set; }

        [ForeignKey(nameof(Chat))]
        public Guid ChatId { get; set; }
        public ChatDAL Chat { get; set; }
    }
}

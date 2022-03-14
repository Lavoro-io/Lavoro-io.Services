using GlobalService.DTO;
using GlobalService.IServices;
using GlobalService.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace GlobalService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ILogger<ChatController> _logger;
        private readonly IChatService _chatService;

        public ChatController(ILogger<ChatController> logger, IChatService chatService)
        {
            _logger = logger;
            _chatService = chatService;
        }

        [HttpGet(nameof(GetChats))]
        public ActionResult<List<ChatDTO>> GetChats(Guid uuid)
        {
            var chats = _chatService.GetChats(uuid);

            if (!chats.Any()) return NotFound("No chats");

            return Ok(chats);
        }

        [HttpGet(nameof(GetChatDetail))]
        public ActionResult<ChatDTO> GetChatDetail(Guid chatId)
        {
            var chat = _chatService.GetChat(chatId);

            return chat;
        }

        [HttpPost(nameof(NewChat))]
        public ActionResult<ChatDTO> NewChat(List<Guid> uuids)
        {
            var chat = _chatService.AddChat(uuids);

            if (chat == null) return Ok("This chat already exist");

            return chat;
        }
    }
}

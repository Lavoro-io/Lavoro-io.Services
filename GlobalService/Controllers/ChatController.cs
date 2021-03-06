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

            if (chats == null || !chats.Any()) return NotFound("No chats");

            return Ok(chats);
        }

        [HttpGet(nameof(GetChatDetail))]
        public ActionResult<ChatDTO> GetChatDetail(Guid chatId)
        {
            var chat = _chatService.GetChat(chatId);

            return chat;
        }

        [HttpPost(nameof(NewChat))]
        public ActionResult NewChat(List<Guid> uuids, ChatType chatType)
        {
            _chatService.AddChat(uuids, chatType);

            return Ok();
        }

        [HttpDelete(nameof(RemoveChat))]
        public ActionResult RemoveChat(Guid chatId)
        {
            _chatService.RemoveChat(chatId);

            return Ok();
        }

        [HttpGet(nameof(GetMessages))]
        public ActionResult<List<MessageDTO>> GetMessages(Guid chatId)
        {
            var messages = _chatService.GetMessages(chatId);

            if (!messages.Any()) return NotFound("No messages found");

            return messages;
        }
    }
}

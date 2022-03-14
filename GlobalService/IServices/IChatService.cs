using GlobalService.DTO;

namespace GlobalService.IServices
{
    public interface IChatService
    {
        MessageDTO AddMessage(Guid userId, Guid chatId, string message);
        ChatDTO AddChat(List<Guid> uuids);
        ChatDTO GetChat(Guid chatId);
        List<ChatDTO> GetChats(Guid uuid);
        List<MessageDTO> GetMessages(Guid chatId);
    }
}

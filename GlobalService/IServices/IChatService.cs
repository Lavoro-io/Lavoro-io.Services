using GlobalService.DTO;
using GlobalService.Utilities;

namespace GlobalService.IServices
{
    public interface IChatService
    {
        MessageDTO AddMessage(Guid userId, Guid chatId, string message);
        void AddChat(List<Guid> uuids, ChatType chatType);
        void RemoveChat(Guid chatId);
        ChatDTO GetChat(Guid chatId);
        List<ChatDTO> GetChats(Guid uuid);
        List<MessageDTO> GetMessages(Guid chatId);
    }
}

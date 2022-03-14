using GlobalService.DAL;
using GlobalService.DTO;
using GlobalService.IServices;
using GlobalService.Utilities;
using Microsoft.EntityFrameworkCore;

namespace GlobalService.Services
{
    public class ChatService : IChatService
    {
        private readonly GloabalContext _dbContext;

        public ChatService(GloabalContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddChat(List<Guid> uuids, ChatType chatType)
        {
            var users = _dbContext.Users.Where(x => x.Enabled && uuids.Any(y => y == x.UserId)).ToList();
            var chatName = "";
            users.ForEach(user =>
            {
                chatName += chatName + user.Name + " " + user.Surname + ", ";
            });

            var chat = new ChatDAL()
            {
                ChatType = chatType,
                ChatName = chatName
            };

            _dbContext.Chats.Add(chat);
            _dbContext.SaveChanges();

            users.ForEach(user =>
            {
                var chatUsers = new ChatUsersDAL()
                {
                    ChatId = chat.ChatId,
                    UserId = user.UserId
                };
                _dbContext.ChatUsers.Add(chatUsers);
            });

            _dbContext.SaveChanges();
        }

        public MessageDTO AddMessage(Guid userId, Guid chatId, string message)
        {
            var messageDb = new MessageDAL()
            {
                UserId = userId,
                ChatId = chatId,
                Message = message
            };

            _dbContext.Messages.Add(messageDb);
            _dbContext.SaveChanges();

            return new MessageDTO()
            {
                 UserId = userId,
                 Message = message
            };
        }

        public ChatDTO GetChat(Guid chatId)
        {
            throw new NotImplementedException();
        }

        public List<ChatDTO> GetChats(Guid uuid)
        {
            throw new NotImplementedException();
        }

        public List<MessageDTO> GetMessages(Guid chatId)
        {
            throw new NotImplementedException();
        }
    }
}

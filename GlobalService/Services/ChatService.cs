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
            var users = _dbContext.Users.Where(x => x.IsActive && uuids.Any(y => y == x.UserId)).ToList();
            var chatName = "";
            users.ForEach(user =>
            {
                chatName += user.Name + " " + user.Surname + ", ";
            });

            var chat = new ChatDAL()
            {
                ChatType = chatType,
                ChatName = chatName,
                IsActive = true,
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
            var chat = _dbContext.Chats.Where(x => x.ChatId == chatId).FirstOrDefault();

            if (chat == null) return null;

            var chatdto = new ChatDTO()
            {
                ChatId = chat.ChatId,
                ChatName = chat.ChatName,
                ChatType = chat.ChatType
            };

            return chatdto;
        }

        public List<ChatDTO> GetChats(Guid uuid)
        {
            var chats = _dbContext.ChatUsers.Include(x => x.Chat)
                                            .Where(x => x.Chat.IsActive && x.User.UserId == uuid)
                                            .Select(x => x.Chat)
                                            .ToList();

            if (!chats.Any()) return null;

            var chatsDto = new List<ChatDTO>();
            foreach(var chat in chats)
            {
                var chatdto = new ChatDTO()
                {
                    ChatId = chat.ChatId,
                    ChatName = chat.ChatName,
                    ChatType = chat.ChatType
                };
                chatsDto.Add(chatdto);
            }

            return chatsDto;
        }

        public List<MessageDTO> GetMessages(Guid chatId)
        {
            var messages = _dbContext.Messages.Where(x => x.ChatId == chatId).ToList();

            var messagesDto = new List<MessageDTO>();
            foreach (var message in messages)
            {
                var messageDto = new MessageDTO()
                {
                    MessageId = message.MessageId,
                    UserId = message.UserId,
                    Message = message.Message,
                    CreatedAt = message.CreatedAt.Ticks,
                    UpdatedAt = message.LastUpdate.Ticks
                };
                messagesDto.Add(messageDto);
            };

            return messagesDto;
        }

        public void RemoveChat(Guid chatId)
        {
            var chat = _dbContext.Chats.Where(x => x.ChatId == chatId).FirstOrDefault();

            chat.IsActive = false;

            _dbContext.Chats.Update(chat);
            _dbContext.SaveChanges();
        }
    }
}

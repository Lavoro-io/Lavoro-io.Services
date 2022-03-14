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

        public ChatDTO AddChat(List<Guid> uuids)
        {
            throw new NotImplementedException();
        }

        public MessageDTO AddMessage(Guid userId, Guid chatId, string message)
        {
            throw new NotImplementedException();
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

        //public ChatDTO AddChat(List<Guid> uuids)
        //{
        //    var chat = _dbContext.Chats.Where(x => uuids.Any(y => y == x.UserId))?.Count();

        //    if(chat > 1) return null;

        //    var firstUuid = uuids.First();

        //    var chatDal = new ChatDAL()
        //    {
        //        UserId = firstUuid
        //    };
        //    _dbContext.Chats.Add(chatDal);
        //    _dbContext.SaveChanges();

        //    uuids.Remove(firstUuid);

        //    var chatDals = new List<ChatDAL>();
        //    uuids.ForEach(uuid =>
        //    {
        //        chatDals.Add(new ChatDAL()
        //        {
        //            ChatCode = chatDal.ChatCode,
        //            UserId = uuid
        //        });
        //    });

        //    _dbContext.AddRange(chatDals);
        //    _dbContext.SaveChanges();

        //    var chatDto = new ChatDTO();
        //    chatDto.ChatCode = chatDal.ChatCode;
        //    chatDto.Users = new List<UserDTO>();
        //    chatDals.ForEach(chat =>
        //    {
        //        chatDto.Users.Add(new UserDTO()
        //        {
        //            UserId = chat.UserId
        //        });
        //    });

        //    return chatDto;
        //}

        //public MessageDTO AddMessage(Guid userId, Guid chatCode, string message)
        //{
        //    var chat = _dbContext.Chats.Where(x => x.ChatCode == chatCode).First();

        //    var dbMessage = new MessageDAL()
        //    {
        //        Message = message,
        //        UserId = userId,
        //        Chat = chat,
        //        ChatCode = chatCode
        //    };

        //    _dbContext.Messages.Add(dbMessage);
        //    _dbContext.SaveChanges();

        //    var messageDto = new MessageDTO()
        //    {
        //        MessageId = dbMessage.MessageId,
        //        ChatCode = dbMessage.ChatCode,
        //        Message = dbMessage.Message,
        //        UserId = dbMessage.UserId,
        //        CreatedAt = dbMessage.CreatedAt.Ticks,
        //        UpdatedAt = dbMessage.LastUpdate.Ticks
        //    };

        //    return messageDto;
        //}

        //public ChatDTO GetChat(Guid chatCode)
        //{
        //    var chats = _dbContext.Chats.Include(x => x.User)
        //                                .Where(x => x.ChatCode == chatCode)
        //                                .ToList();

        //    var chatDto = new ChatDTO();
        //    chatDto.ChatCode = chatCode;
        //    chatDto.Users = new List<UserDTO>();
        //    chats.ForEach(chat =>
        //    {
        //        chatDto.Users.Add(new UserDTO()
        //        {
        //            UserId = chat.User.UserId,
        //            Name = chat.User.Name,
        //            Surname = chat.User.Surname,
        //            Username = chat.User.Username
        //        });
        //    });

        //    return chatDto;
        //}

        //public List<ChatDTO> GetChats(Guid uuid)
        //{
        //    var chats = _dbContext.Chats.Where(x => x.UserId == uuid)
        //                                .Distinct()
        //                                .ToList();

        //    var chatsDto = new List<ChatDTO>();
        //    chats.ForEach(chat =>
        //    {
        //        chatsDto.Add(new ChatDTO()
        //        {
        //            ChatCode = chat.ChatCode,
        //        });
        //    });

        //    //ensure to return no-multiple elements
        //    return chatsDto.Distinct().ToList();
        //}

        //public List<MessageDTO> GetMessages(Guid chatCode)
        //{
        //    var messages = _dbContext.Messages.Where(x => x.ChatCode == chatCode).ToList();

        //    var messagesDto = new List<MessageDTO>();
        //    messages.ForEach(message =>
        //    {
        //        messagesDto.Add(new MessageDTO()
        //        {
        //            MessageId = message.MessageId,
        //            ChatCode = message.ChatCode,
        //            UserId = message.UserId,
        //            Message = message.Message,
        //            CreatedAt = message.CreatedAt.Ticks,
        //            UpdatedAt = message.LastUpdate.Ticks
        //        });
        //    });

        //    return messagesDto;
        //}
    }
}

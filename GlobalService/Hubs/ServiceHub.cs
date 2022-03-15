using GlobalService.DTO;
using GlobalService.IServices;
using GlobalService.Utilities;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalService.Hubs
{
    [Authorize]
    public class ServiceHub : Hub
    {
        private readonly static HubManager<string> _connections =
            new HubManager<string>();
        private readonly IUserService _userService;
        private readonly IChatService _chatService;

        public ServiceHub(IUserService userService, IChatService chatService)
        {
            this._userService = userService;
            this._chatService = chatService;
        }

        private UserDTO GetUserContext()
        {
            var httpContext = Context.GetHttpContext();
            var userId = httpContext?.Request.Query["uuid"];
            var user = _userService.GetUser(new Guid(userId));
            return user;
        }

        public override Task OnConnectedAsync()
        {
            var user = GetUserContext();

            if (!_connections.GetConnections(user.UserId.ToString()).Contains(Context.ConnectionId))
            {
                _connections.Add(user.UserId.ToString(), Context.ConnectionId);
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var user = GetUserContext();

            _connections.Remove(user.UserId.ToString(), Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }

        //public void JoinChat(string chatId)
        //{
        //    var sender = GetUserContext();

        //    Groups.AddToGroupAsync(sender.UserId.ToString(), chatId).RunSynchronously();
        //}

        //public void LeaveChat(string chatId)
        //{
        //    var sender = GetUserContext();

        //    Groups.RemoveFromGroupAsync(sender.UserId.ToString(), chatId).RunSynchronously();
        //}

        //public void SendChatMessage(string chatId, string message)
        //{
        //    var sender = GetUserContext();

        //    var chat = _chatService.GetChat(new Guid(chatId));
        //    var _message = _chatService.AddMessage(sender.UserId, chat.ChatId, message);

        //    Clients.Group(chat.ChatId.ToString()).SendAsync("addChatMessage", _message);      
        //}
    }
}

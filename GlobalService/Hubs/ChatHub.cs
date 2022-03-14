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
    public class ChatHub : Hub
    {
        private readonly static HubManager<string> _connections =
            new HubManager<string>();
        private readonly IUserService _userService;
        private readonly IChatService _chatService;

        public ChatHub(IUserService userService, IChatService chatService)
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

        public void JoinChat(string chatCode)
        {
            var sender = GetUserContext();

            Groups.AddToGroupAsync(sender.UserId.ToString(), chatCode).RunSynchronously();
        }

        public void LeaveChat(string chatCode)
        {
            var sender = GetUserContext();

            Groups.RemoveFromGroupAsync(sender.UserId.ToString(), chatCode).RunSynchronously();
        }

        public void SendChatMessage(string chatCode, string message)
        {
            var sender = GetUserContext();

            var chat = _chatService.GetChat(new Guid(chatCode));
            var _message = _chatService.AddMessage(sender.UserId, chat.ChatCode, message);

            Clients.Group(chat.ChatCode.ToString()).SendAsync("addChatMessage", _message);      
        }
    }
}

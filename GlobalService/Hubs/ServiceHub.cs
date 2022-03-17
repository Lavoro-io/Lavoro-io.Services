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

        #region ChatService
        public void JoinChat(string chatId)
        {
            var sender = GetUserContext();

            var connectionId = _connections.GetConnection(sender.UserId.ToString());

            Groups.AddToGroupAsync(connectionId, chatId);
        }

        public void LeaveChat(string chatId)
        {
            var sender = GetUserContext();

            var connectionId = _connections.GetConnection(sender.UserId.ToString());

            Groups.RemoveFromGroupAsync(connectionId, chatId);
        }

        public void SendChatMessage(string chatId, string message)
        {
            var sender = GetUserContext();
            var _chatId = new Guid(chatId);

            var _message = _chatService.AddMessage(sender.UserId, _chatId, message);

            Clients.Group(_chatId.ToString()).SendAsync("addChatMessage", _message);
        }
        #endregion
    }
}

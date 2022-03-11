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

        public ChatHub(IUserService userService)
        {
            this._userService = userService;
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

        public void SendChatMessage(string userId, string message)
        {
            var sender = GetUserContext();

            var connectionId = _connections.GetConnection(userId);

            var _message = sender.Username + ": " + message;
            Clients.Client(connectionId).SendAsync("addChatMessage", _message);      
        }
    }
}

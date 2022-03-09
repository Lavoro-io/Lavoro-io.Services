using GlobalService.IServices;
using GlobalService.Utilities;
using Microsoft.AspNetCore.SignalR;

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

        private string GetUserIdContext()
        {
            var httpContext = Context.GetHttpContext();
            var userId = httpContext?.Request.Query["uuid"];
            return userId;
        }

        public override Task OnConnectedAsync()
        {
            var userId = GetUserIdContext();

            if (!_connections.GetConnections(userId).Contains(Context.ConnectionId))
            {
                _connections.Add(userId, Context.ConnectionId);
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = GetUserIdContext();

            _connections.Remove(userId, Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }

        public void SendChatMessage(string userId, string message)
        {
            var senderId = GetUserIdContext();

            var connectionId = _connections.GetConnection(userId);

            var _message = senderId + ": " + message;
            Clients.Client(connectionId).SendAsync("addChatMessage", _message);      
        }
    }
}

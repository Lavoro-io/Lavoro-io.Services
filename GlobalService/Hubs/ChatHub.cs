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

        public override Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var user = httpContext?.Items["user"];

            // if (!_connections.GetConnections(name).Contains(Context.ConnectionId))
            // {
            //     _connections.Add(name, Context.ConnectionId);
            // }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            string name = Context.User.Identity.Name;

            _connections.Remove(name, Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }

        public void SendChatMessage(string who, string message)
        {
            string name = Context.User.Identity.Name;

            foreach (var connectionId in _connections.GetConnections(who))
            {
                Clients.Client(connectionId).SendAsync("addChatMessage", name + ": " + message);
            }
        }
    }
}

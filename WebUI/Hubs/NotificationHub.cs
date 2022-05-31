using Microsoft.AspNetCore.SignalR;

namespace WebUI.Hubs
{
    public class NotificationHub : Hub
    {
        public string GetConnectionId() => Context.ConnectionId;
    }
}

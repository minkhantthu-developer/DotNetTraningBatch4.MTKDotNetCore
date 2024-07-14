using Microsoft.AspNetCore.SignalR;

namespace MKTDotNetCore.RealtimeChatApp.Hubs
{
    public class ChatHub:Hub
    {
        public async Task ServerReciveMessage(string user,string message)
        {
            await Clients.All.SendAsync("ClientReceiveMessage", user, message);
        }
    }
}

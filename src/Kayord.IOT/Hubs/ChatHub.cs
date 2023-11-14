using Microsoft.AspNetCore.SignalR;

namespace Kayord.IOT.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessage(string message)
    {
        // await Clients.All.SendMessage("ReceiveMessage", message);
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
}

public interface IChat
{
    Task SendMessage(string test, string message);
}
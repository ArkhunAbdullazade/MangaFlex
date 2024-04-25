using Microsoft.AspNetCore.SignalR;

namespace MangaFlex.Api.Hubs;

public class ProfileHub : Hub
{
    public async Task SendMessage()
    {
        await Clients.All.SendAsync("ReceiveMessage", "Data Change", "Data of page have been change");
    }
}

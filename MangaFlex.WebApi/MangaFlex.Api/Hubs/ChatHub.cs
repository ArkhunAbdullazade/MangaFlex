using MangaFlex.Core.Data.Message.Models;
using Microsoft.AspNetCore.SignalR;

namespace MangaFlex.Api.Hubs;

public class ProfileHub : Hub
{
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
}

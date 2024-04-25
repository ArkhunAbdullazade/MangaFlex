using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace MangaFlexFront.Data.SignalR;

public class ChatService
{
    private HubConnection _hubConnection;

    public event Action<string, string> OnReceiveMessage;

    public async Task ConnectAsync(string hubUrl)
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(hubUrl)
            .Build();

        _hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            OnReceiveMessage?.Invoke(user, message);
        });

        await _hubConnection.StartAsync();
    }

    public async Task SendMessageAsync(string user, string message)
    {
        await _hubConnection.SendAsync("SendMessage", user, message);
    }
}
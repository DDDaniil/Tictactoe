using Microsoft.AspNetCore.SignalR;

namespace Tictactoe.API.Hubs;

public class GameHub : Hub
{
    public async Task Send(string message)
    {
        await this.Clients.All.SendAsync("Receive", message);
    }
}
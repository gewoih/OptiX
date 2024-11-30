using Microsoft.AspNetCore.SignalR;

namespace OptiX.Application.SignalR;

public class MarketDataHub : Hub
{
    public async Task SubscribeToSymbol(string symbol)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, symbol);
        await Clients.Caller.SendAsync("ReceiveMessage", $"Subscribed to {symbol} updates.");
    }

    public async Task UnsubscribeFromSymbol(string symbol)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, symbol);
        await Clients.Caller.SendAsync("ReceiveMessage", $"Unsubscribed from {symbol} updates.");
    }
}
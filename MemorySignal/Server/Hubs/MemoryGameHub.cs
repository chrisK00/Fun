using MemorySignal.Shared.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace MemorySignal.Server.Hubs;
public class MemoryGameHub : Hub<IMemoryGameClient>, IMemoryGameHub
{
    public Task Flip(string gameId, int cardId)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<PlayerResponse>> Join(string gameId, int cardCollectionId, string playerName)
    {
        throw new NotImplementedException();
    }

    public Task Start(string gameId)
    {
        throw new NotImplementedException();
    }
}

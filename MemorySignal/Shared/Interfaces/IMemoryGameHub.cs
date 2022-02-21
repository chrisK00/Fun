using MemorySignal.Shared.Responses;

namespace MemorySignal.Shared.Interfaces;

public interface IMemoryGameHub
{
    const string Uri = "/memoryGameHub";

    Task Flip(string gameId, string cardId);

    Task Start(string gameId);

    /// <summary>
    /// Joins an existing game or creates one if there isnt one with the <paramref name="gameId"/>
    /// </summary>
    /// <param name="gameId"></param>
    /// <param name="cardCollectionId"></param>
    /// <param name="playerName"></param>
    /// <returns>Players that have already joined</returns>
    Task<ICollection<PlayerResponse>> Join(string gameId, int cardCollectionId, string playerName);
}
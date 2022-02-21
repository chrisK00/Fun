using System.Collections.Concurrent;

namespace MemorySignal.Core.Managers;

public class MemoryGameManager : IMemoryGameManager
{
    private readonly ConcurrentDictionary<string, MemoryGame> _games = new();

    public bool Add(MemoryGame game) => _games.TryAdd(game.Id, game);

    public MemoryGame Get(string id) => _games.GetValueOrDefault(id);

    public MemoryGame GetByUserId(string id) => _games.Values.FirstOrDefault(g => g.Players.Any(p => p.Id == id));

    public bool Remove(string id) => _games.TryRemove(id, out _);
}
namespace MemorySignal.Core.Interfaces;
public interface IMemoryGameManager
{
    bool Add(MemoryGame game);
    MemoryGame GetByUserId(string id);
    MemoryGame Get(string id);
    bool Remove(string id);
}

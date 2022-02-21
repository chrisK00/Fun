namespace MemorySignal.Shared.Responses;
public class PlayerResponse
{
    public PlayerResponse(string name)
    {
        Name = name;
    }

    public string Name { get; }
}

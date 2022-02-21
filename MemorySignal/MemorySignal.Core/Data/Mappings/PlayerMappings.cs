namespace MemorySignal.Core.Data.Mappings;

public static class PlayerMappings
{
    public static PlayerResponse ToResponse(this Player player) => new(player.Name);

    public static IEnumerable<PlayerResponse> ToResponses(this IEnumerable<Player> players) =>
        players.Select(p => p.ToResponse())
        .ToArray();

    public static IEnumerable<string> Ids(this IEnumerable<Player> players, string exceptId = "") => 
        players.Where(p => p.Id != exceptId)
        .Select(p => p.Id)
        .ToArray();
}
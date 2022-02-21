namespace MemorySignal.Core.Data.Mappings;

public static class CardMappings
{
    public static MemoryCardResponse ToResponse(this Card card) => new(card.TempId, card.ImageUrl);

    public static IEnumerable<MemoryCardResponse> ToResponses(this IEnumerable<Card> cards) =>
        cards.Select(c => c.ToResponse())
        .ToArray();
}
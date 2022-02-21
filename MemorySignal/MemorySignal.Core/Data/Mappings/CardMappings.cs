namespace MemorySignal.Core.Data.Mappings;

public static class CardMappings
{
    public static CardResponse ToResponse(this Card card) => new(card.TempId, card.ImageUrl);

    public static IEnumerable<CardResponse> ToResponses(this IEnumerable<Card> cards) =>
        cards.Select(c => c.ToResponse())
        .ToArray();
}
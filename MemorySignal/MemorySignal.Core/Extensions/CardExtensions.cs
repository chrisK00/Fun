namespace MemorySignal.Core.Extensions;
public static class CardExtensions
{
    public static IEnumerable<Card> Duplicate(this IEnumerable<Card> cards)
    {
        var duplicatedCards = new List<Card>(cards);
        foreach (var card in cards)
        {
            duplicatedCards.Add(card.Copy());
        }

        return duplicatedCards;
    }
}

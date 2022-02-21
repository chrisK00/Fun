namespace MemorySignal.Shared.Responses;

public class CardCollectionResponse
{
    public CardCollectionResponse(int id, string name, int cardsCount, string firstCardImageUrl)
    {
        Id = id;
        Name = name;
        CardsCount = cardsCount;
        FirstCardImageUrl = firstCardImageUrl;
    }

    public int Id { get; }
    public string Name { get; }
    public int CardsCount { get; }
    public string FirstCardImageUrl { get; }
}
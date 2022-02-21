namespace MemorySignal.Shared.Responses;

public class CardCollectionResponse
{
    public CardCollectionResponse(int id, string name, int cardsCount)
    {
        Id = id;
        Name = name;
        CardsCount = cardsCount;
    }

    public int Id { get; }
    public string Name { get; }
    public int CardsCount { get; }
}
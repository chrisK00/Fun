namespace MemorySignal.Core.Interfaces;

public interface ICardCollectionQueries
{
    IEnumerable<CardCollectionResponse> GetAllForList();
    IEnumerable<Card> GetCards(int collectionId);
}
namespace MemorySignal.Core.Interfaces;

public interface ICardCollectionQueries
{
    Task<IEnumerable<CardCollectionResponse>> GetAllForList();
    Task<IEnumerable<Card>> GetCards(int collectionId);
}
namespace MemorySignal.Core.Interfaces;

public interface ICardCollectionQueries
{
    Task<IEnumerable<CardCollectionResponse>> GetAll();
    Task<IEnumerable<Card>> GetCards(int collectionId);
}
namespace MemorySignal.Core.Interfaces;

public interface ICardCollectionQueries
{
    IEnumerable<CardCollectionResponse> GetAll();
    IEnumerable<CardResponse> GetCards(int collectionId);
}
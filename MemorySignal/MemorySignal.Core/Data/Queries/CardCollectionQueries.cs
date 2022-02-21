namespace MemorySignal.Core.Data.Queries;

public class CardCollectionQueries : ICardCollectionQueries
{
    private readonly DataContext _context;

    public CardCollectionQueries(DataContext context)
    {
        _context = context;
    }

    public IEnumerable<CardCollectionResponse> GetAll()
    {
        return _context.CardCollections
            .ToResponses();
    }

    public IEnumerable<CardResponse> GetCards(int collectionId)
    {
        return _context.CardCollections
             .Where(cc => cc.Id == collectionId)
             .Select(cc => cc.Cards)
             .FirstOrDefault()
             .ToResponses();
    }
}
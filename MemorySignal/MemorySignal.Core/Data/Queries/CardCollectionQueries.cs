namespace MemorySignal.Core.Data.Queries;

public class CardCollectionQueries : ICardCollectionQueries
{
    private readonly DataContext _context;

    public CardCollectionQueries(DataContext context)
    {
        _context = context;
    }

    public IEnumerable<CardCollectionResponse> GetAllForList()
    {
        return _context.CardCollections.AsNoTracking()
            .ToResponses();
    }

    public IEnumerable<Card> GetCards(int collectionId)
    {
        return _context.CardCollections.AsNoTracking()
            .FirstOrDefault()?.Cards;
    }
}
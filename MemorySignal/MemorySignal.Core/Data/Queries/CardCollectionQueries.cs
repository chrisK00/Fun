namespace MemorySignal.Core.Data.Queries;

public class CardCollectionQueries : ICardCollectionQueries
{
    private readonly DataContext _context;

    public CardCollectionQueries(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CardCollectionResponse>> GetAllForList()
    {
        return await _context.CardCollections.AsNoTracking()
            .Select(cc => cc.ToResponse())
            .ToArrayAsync();
    }

    public async Task<IEnumerable<Card>> GetCards(int collectionId)
    {
        return await _context.CardCollections.AsNoTracking()
            .Where(cc => cc.Id == collectionId)
            .Select(cc => cc.Cards)
            .FirstOrDefaultAsync();
    }
}
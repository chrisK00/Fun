namespace MemorySignal.Core.Data.Mappings;

public static class CardCollectionMappings
{
    public static IEnumerable<CardCollectionResponse> ToResponses(this IEnumerable<CardCollection> collection) => collection.Select(c => c.ToResponse());

    public static CardCollectionResponse ToResponse(this CardCollection collection) => new(collection.Id, collection.Name, collection.Cards.Count);
}
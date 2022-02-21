namespace MemorySignal.Core.Data.Mappings;

public static class CardCollectionMappings
{
    public static CardCollectionResponse ToResponse(this CardCollection collection) => new(collection.Id, collection.Name, collection.Cards.Count);
}
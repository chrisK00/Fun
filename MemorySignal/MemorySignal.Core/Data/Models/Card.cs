namespace MemorySignal.Core.Data.Models;

public class Card
{
    public Card(int id, string imageUrl, string apiId)
    {
        Id = id;
        ImageUrl = imageUrl;
        ApiId = apiId;
    }

    public int Id { get; private set; }
    public string ImageUrl { get; private set; }
    public string ApiId { get; private set; }
    public IEnumerable<CardCollection> CardCollections { get; private set; }

    public Card Copy() => new(Id, ImageUrl, ApiId);
}
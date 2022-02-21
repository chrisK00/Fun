namespace MemorySignal.Core.Data.Models;

public class Card
{
    public Card(string imageUrl, string apiId)
    {
        ImageUrl = imageUrl;
        ApiId = apiId;
    }

    public int Id { get; init; }
    public string TempId { get; } = Guid.NewGuid().ToString();
    public string ImageUrl { get; private set; }
    public string ApiId { get; private set; }
    public IEnumerable<CardCollection> CardCollections { get; private set; }

    public Card Copy() => new(ImageUrl, ApiId) { Id = Id };
}
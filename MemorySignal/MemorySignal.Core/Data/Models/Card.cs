namespace MemorySignal.Core.Data.Models;

public class Card
{
    public Card(int id, string imageUrl, string apiId)
    {
        Id = id;
        ImageUrl = imageUrl;
        ApiId = apiId;
    }

    public int Id { get; }
    public string ImageUrl { get; }
    public string ApiId { get; }
}
namespace MemorySignal.Shared.Responses;

public class CardResponse
{
    public CardResponse(int id, string imageUrl)
    {
        Id = id;
        ImageUrl = imageUrl;
    }

    public int Id { get; }
    public string ImageUrl { get; }
    public bool IsFlipped { get; set; }
}

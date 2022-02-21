namespace MemorySignal.Shared.Responses;

public class CardResponse
{
    public CardResponse(string tempId, string imageUrl)
    {
        TempId = tempId;
        ImageUrl = imageUrl;
    }

    public string TempId { get; }
    public string ImageUrl { get; }
    public bool IsFlipped { get; set; }
}

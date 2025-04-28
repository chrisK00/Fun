namespace MemorySignal.Shared.Responses;
public class RemoveCardCollectionResponse
{
    public int ImagesDeletedCount { get; set; }

    public RemoveCardCollectionResponse(int imagesDeletedCount)
    {
        ImagesDeletedCount = imagesDeletedCount;
    }
}

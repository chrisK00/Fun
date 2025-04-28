namespace MemorySignal.Shared.Requests;
public class RemoveCardCollectionRequest
{
    public int Id { get; set; }

    public RemoveCardCollectionRequest(int id)
    {
        Id = id;
    }
}

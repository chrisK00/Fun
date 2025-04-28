using MemorySignal.Shared.Requests;
using Microsoft.Extensions.Logging;

namespace MemorySignal.Core.Actions;
public class RemoveCardCollectionAction : ActionBase<RemoveCardCollectionRequest, RemoveCardCollectionResponse>
{

    private readonly IImageManager _imageManager;
    private readonly DataContext _db;
    private readonly ILogger<AddCardCollectionAction> _logger;

    public RemoveCardCollectionAction(IImageManager imageManager, DataContext db, ILogger<AddCardCollectionAction> logger)
    {
        _imageManager = imageManager;
        _db = db;
        _logger = logger;
    }

    public override async Task<RemoveCardCollectionResponse> Execute(RemoveCardCollectionRequest request)
    {
        _logger.LogInformation("Executing Remove Card Collection Action");
        var cardCollectionToRemove = await _db.CardCollections.Include(cc => cc.Cards).FirstOrDefaultAsync(x => x.Id == request.Id);

        if (cardCollectionToRemove == null)
        {
            throw new KeyNotFoundException($"Could not find a card collection by the specified id: {request.Id}");
        }

        var cardIds = cardCollectionToRemove.Cards.Select(c => c.ApiId).ToArray();
        var deleteResult = await _imageManager.BulkDeleteAsync(cardIds);
        if ((int)deleteResult.StatusCode is > 299 or < 200)
        {
            var errorMessage = "Failed to fully remove card collection from cloud provider";
            _logger.LogError(errorMessage + ". Error: {Error}", deleteResult.Error.Message);
            AddError(errorMessage);
            return deleteResult.Partial
                ? new RemoveCardCollectionResponse(deleteResult.DeletedCounts.Count)
                : new RemoveCardCollectionResponse(0);
        }

        _db.Remove(cardCollectionToRemove);
        await _db.SaveChangesAsync();

        return new RemoveCardCollectionResponse(deleteResult.DeletedCounts.Count);
    }
}

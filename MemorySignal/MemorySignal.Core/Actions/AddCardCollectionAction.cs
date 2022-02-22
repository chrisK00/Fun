using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MemorySignal.Shared.Requests;
using Microsoft.Extensions.Logging;

namespace MemorySignal.Core.Actions;

public class AddCardCollectionAction : ActionBase<AddCardCollectionRequest, CardCollectionResponse>
{
    private readonly IImageManager _imageManager;
    private readonly DataContext _db;
    private readonly ILogger<AddCardCollectionAction> _logger;

    public AddCardCollectionAction(IImageManager imageManager, DataContext db, ILogger<AddCardCollectionAction> logger)
    {
        _imageManager = imageManager;
        _db = db;
        _logger = logger;
    }

    public override async Task<CardCollectionResponse> Execute(AddCardCollectionRequest request)
    {
        _logger.LogInformation("Executing Add Card Collection Action");

        var uploadTasks = request.FilePaths
            .Select(path => _imageManager.UploadAsync(CreateParams(path)))
            .ToList();

        var cards = new List<Card>();
        foreach (var task in uploadTasks)
        {
            var result = await task;
            cards.Add(new Card(result.Url.AbsoluteUri, result.PublicId));
        }

        var collection = new CardCollection(request.Name, cards);
        _db.Add(collection);
        await _db.SaveChangesAsync();

        return collection.ToResponse();
    }

    private ImageUploadParams CreateParams(string imagePath)
    {
        return new ImageUploadParams
        {
            File = new FileDescription(imagePath),
            AssetFolder = Const.CardImagesFolderName,
            Transformation = new Transformation().Height(100).Width(100).Crop("fill")
        };
    }
}
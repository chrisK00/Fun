using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MemorySignal.Shared.Requests;
using Microsoft.AspNetCore.Http;
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
        var streams = new List<Stream>();
        ICollection<Card> cards = new List<Card>();

        try
        {
            var tasks = request.Images.Select(f => HandleUploadImage(f, streams)).ToList();
            cards = await Task.WhenAll(tasks);
        }
        finally
        {
            var tasks = streams.Select(s => s.DisposeAsync().AsTask()).ToList();
            await Task.WhenAll(tasks);
        }

        var collection = new CardCollection(request.Name, cards);
        _db.Add(collection);
        await _db.SaveChangesAsync();

        return collection.ToResponse();
    }

    private ImageUploadParams CreateParams(string fileName, Stream stream)
    {
        return new ImageUploadParams
        {
            File = new FileDescription(fileName, stream),
            Folder = Const.CardImagesFolderName,
            Transformation = new Transformation().Height(100).Width(100)
        };
    }

    private async Task<Card> HandleUploadImage(IFormFile file, List<Stream> streams)
    {
        var stream = file.OpenReadStream();
        streams.Add(stream);
        var result = await _imageManager.UploadAsync(CreateParams(file.Name, stream));

        return new Card(result.Url.AbsoluteUri, result.PublicId);
    }
}
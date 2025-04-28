using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MemorySignal.Core.Options;
using Microsoft.Extensions.Options;

namespace MemorySignal.Core.Managers;

public class ImageManager : IImageManager
{
    private readonly ImageApiOptions _apiOptions;


    private Cloudinary _client;

    private Cloudinary Client => _client ??= new Cloudinary(
        new Account(_apiOptions.CloudName, _apiOptions.Key, _apiOptions.Secret)
    );

    public ImageManager(IOptions<ImageApiOptions> options)
    {
        _apiOptions = options.Value;
    }

    public async Task<ImageUploadResult> UploadAsync(ImageUploadParams param)
    {
        return await Client.UploadAsync(param);
    }

    public async Task<DelResResult> BulkDeleteAsync(string[] apiIds)
    {
        return await Client.DeleteResourcesAsync(ResourceType.Image, apiIds);
    }
}
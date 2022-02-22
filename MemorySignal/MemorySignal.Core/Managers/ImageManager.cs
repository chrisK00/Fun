using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MemorySignal.Core.Options;
using Microsoft.Extensions.Options;

namespace MemorySignal.Core.Managers;

public class ImageManager : IImageManager
{
    private readonly ImageApiOptions _apiOptions;

    public ImageManager(IOptions<ImageApiOptions> options)
    {
        _apiOptions = options.Value;
    }

    public async Task<ImageUploadResult> UploadAsync(ImageUploadParams param)
    {
        var account = new Account { ApiSecret = _apiOptions.Secret, ApiKey = _apiOptions.Key, Cloud = _apiOptions.CloudName };
        var client = new Cloudinary(account);
        return await client.UploadAsync(param);
    }
}
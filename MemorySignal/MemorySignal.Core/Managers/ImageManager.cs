using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MemorySignal.Core.Options;
using Microsoft.Extensions.Options;

namespace MemorySignal.Core.Managers;

// TODO might need to replace with local opt
public class ImageManager : IImageManager
{
    private readonly ImageApiOptions _apiOptions;

    public ImageManager(IOptions<ImageApiOptions> options)
    {
        _apiOptions = options.Value;
    }

    public async Task<ImageUploadResult> UploadAsync(ImageUploadParams param)
    {
        var account = new Account(_apiOptions.CloudName, _apiOptions.Key, _apiOptions.Secret);
        var client = new Cloudinary(account);
        return await client.UploadAsync(param);
    }
}
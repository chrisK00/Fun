using CloudinaryDotNet.Actions;

namespace MemorySignal.Core.Interfaces;

public interface IImageManager
{
    Task<ImageUploadResult> UploadAsync(ImageUploadParams param);
}
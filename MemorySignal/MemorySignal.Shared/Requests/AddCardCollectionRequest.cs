using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MemorySignal.Shared.Requests;

public class AddCardCollectionRequest
{
    [Required, MinLength(2)] public string Name { get; set; }
    public IEnumerable<IFormFile> Images { get; set; }
}
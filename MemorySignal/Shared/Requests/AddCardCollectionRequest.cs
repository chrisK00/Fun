using System.ComponentModel.DataAnnotations;

namespace MemorySignal.Shared.Requests;

public class AddCardCollectionRequest
{
    [Required, MinLength(2)] public string Name { get; set; }

}
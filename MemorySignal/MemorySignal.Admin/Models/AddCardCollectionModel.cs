using System.ComponentModel.DataAnnotations;

namespace MemorySignal.Admin.Models;
public class AddCardCollectionModel
{
    [Required, MinLength(2)] public string Name { get; set; }
    [Required] public string FolderPath { get; set; }
}

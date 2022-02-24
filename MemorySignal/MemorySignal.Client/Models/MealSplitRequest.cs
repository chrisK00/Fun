using System.ComponentModel.DataAnnotations;

namespace MemorySignal.Client.Models;
public class MealSplitRequest
{
    [Required, Range(1, int.MaxValue, ErrorMessage = "Enter a minimum of 1 gram")]
    public int Gram { get; set; }

    [Required, Range(2, int.MaxValue, ErrorMessage = "Split it in to at least 2 portions")]
    public int Portions { get; set; }
}

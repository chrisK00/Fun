using System.ComponentModel.DataAnnotations;

namespace MemorySignal.Client.Models;
public class PricePerKgRequest
{
    private const int _gramToKg = 1000;

    [Required, Range(1, int.MaxValue, ErrorMessage = "Enter a minimum of 1 gram")]
    public int Gram { get; set; }

    [Required, Range(1, int.MaxValue, ErrorMessage = "Enter a minimum price of 1")]
    public int Price { get; set; }
}

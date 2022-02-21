namespace MemorySignal.Core.Data.Models;

public class CardCollection
{
    public CardCollection(int id, string name, ICollection<Card> cards)
    {
        Id = id;
        Name = name;
        Cards = cards;
    }

    public int Id { get; }
    public string Name { get; set; }
    public ICollection<Card> Cards { get; }
}
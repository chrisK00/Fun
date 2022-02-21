namespace MemorySignal.Core.Data.Models;

public class CardCollection
{
    public CardCollection(string name, ICollection<Card> cards)
    {
        Name = name;
        Cards = cards;
    }

    private CardCollection() { }

    public int Id { get; private set; }
    public string Name { get; set; }
    public ICollection<Card> Cards { get; private set; }
}
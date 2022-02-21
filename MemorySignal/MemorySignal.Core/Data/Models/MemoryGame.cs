namespace MemorySignal.Core.Data.Models;

public class MemoryGame
{
    private readonly List<Player> _players = new();

    public MemoryGame(IEnumerable<Card> cards, string id)
    {
        Cards = cards;
        Id = id;
    }

    public IReadOnlyList<Player> Players => _players;
    public IEnumerable<Card> Cards { get; private set; }
    public string Id { get; }
    public Player PlayersTurn { get; private set; }
    public Card LastFlippedCard { get; private set; }

    /// <summary>
    /// Starts or restarts the game
    /// </summary>
    public void Start()
    {
        PlayersTurn = Players[0];
        LastFlippedCard = null;
        Cards = Cards.Randomize();
    }

    /// <summary>
    /// Flips the given card and compares it to the <see cref="LastFlippedCard"/> if there is one
    /// </summary>
    /// <param name="cardId"></param>
    /// <returns>If <see cref="LastFlippedCard"/> matches <paramref name="cardId"/></returns>
    public bool Flip(int cardId)
    {
        var card = Cards.First(c => c.Id == cardId);
        // first card to flip
        if (LastFlippedCard is null)
        {
            LastFlippedCard = card;
            return false;
        }

        // we have flipped 2 cards and they are the same
        if (card.Id == LastFlippedCard.Id)
        {
            LastFlippedCard = null;
            return true;
        }

        NextTurn();
        return false;
    }

    public void NextTurn()
    {
        LastFlippedCard = null;
        var playerIndex = _players.IndexOf(PlayersTurn);
        PlayersTurn = playerIndex == Players.LastIndex() ? Players[0] : Players[++playerIndex];
    }

    public void Join(Player player)
    {
        CheckIfGameHasStarted();
        lock (_players)
        {
            _players.Add(player);
        }
    }

    public void RemovePlayer(Player player)
    {
        if (PlayersTurn != null && PlayersTurn.Id == player.Id)
        {
            NextTurn();
        }

        lock (_players)
        {
            _players.Remove(player);
        }
    }

    private void CheckIfGameHasStarted()
    {
        if (PlayersTurn != null) throw new InvalidOperationException("Game has already started");
    }
}
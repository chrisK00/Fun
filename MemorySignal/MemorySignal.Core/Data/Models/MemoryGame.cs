namespace MemorySignal.Core.Data.Models;

public class MemoryGame
{
    private readonly List<Player> _players = new();

    public MemoryGame(string id, IEnumerable<Card> cards)
    {
        Id = id;
        Cards = cards;
    }

    public IReadOnlyList<Player> Players => _players;
    public IEnumerable<Card> Cards { get; private set; }
    public string Id { get; }
    public Player PlayersTurn { get; private set; }
    public Card LastFlippedCard { get; private set; }

    /// <summary>
    /// Starts or restarts the game and sets <see cref="PlayersTurn"/> to <paramref name="playerId"/>
    /// </summary>
    /// <param name="playerId">Player trying to start the game</param>
    public void Start(string playerId)
    {
        var player = Players.FirstOrDefault(p => p.Id == playerId);
        if (player == null) throw new InvalidOperationException("You are not part of this game");
        PlayersTurn = player;
        LastFlippedCard = null;
        Cards = Cards.Randomize();
    }

    /// <summary>
    /// Flips the given card and compares it to the <see cref="LastFlippedCard"/> if there is one
    /// </summary>
    /// <param name="cardId"></param>
    /// <param name="playerId">Player trying to flip</param>
    /// <returns>If <see cref="LastFlippedCard"/> matches <paramref name="cardId"/></returns>
    public bool Flip(string cardId, string playerId)
    {
        if (PlayersTurn.Id != playerId) throw new InvalidOperationException("It is not your turn");

        var card = Cards.First(c => c.TempId == cardId);
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
            if (_players.Any(p => p.Name == player.Name)) throw new ArgumentException($"Name {player.Name} is taken");
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
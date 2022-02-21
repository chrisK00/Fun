using MemorySignal.Shared.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace MemorySignal.Server.Hubs;

public class MemoryGameHub : Hub<IMemoryGameClient>, IMemoryGameHub
{
    private readonly IMemoryGameManager _gameManager;
    private readonly ILogger<MemoryGameHub> _logger;
    private readonly ICardCollectionQueries _cardCollectionQueries;

    public MemoryGameHub(IMemoryGameManager gameManager, ILogger<MemoryGameHub> logger, ICardCollectionQueries cardCollectionQueries)
    {
        _gameManager = gameManager;
        _logger = logger;
        _cardCollectionQueries = cardCollectionQueries;
    }

    private string _connectionId => Context.ConnectionId;

    public async Task Flip(string gameId, int cardId)
    {
        var game = Guard.Null(_gameManager.Get(gameId), gameId, "memory game");
        var connectionIds = game.Players.Ids();

        await Clients.Clients(connectionIds).Flipping(cardId);
        _logger.LogInformation("{@Name} is flipping card {@cardHashCode}", game.PlayersTurn.Name, cardId);

        var isMatch = game.Flip(cardId, _connectionId);
        await Clients.Clients(connectionIds).Turn(game.PlayersTurn.Name, isMatch);
    }

    public async Task<ICollection<PlayerResponse>> Join(string gameId, int cardCollectionId, string playerName)
    {
        var player = new Player(_connectionId, playerName);
        var game = _gameManager.Get(gameId);

        if (game == null)
        {
            var cards = await _cardCollectionQueries.GetCards(cardCollectionId);
            game = new MemoryGame(gameId, Guard.Empty(cards, cardCollectionId).Duplicate());
            _gameManager.Add(game);
        }

        _logger.LogInformation("{@playerName} is joining a game", playerName);
        game.Join(player);
        await Clients.Clients(game.Players.Ids(_connectionId)).PlayerJoined(player.ToResponse());

        return game.Players.ToResponses();
    }

    public async Task Start(string gameId)
    {
        var game = _gameManager.Get(gameId);
        game.Start(_connectionId);
        _logger.LogInformation("{@Name} started game {@gameId}", game.PlayersTurn.Name, gameId);
        await Clients.Clients(game.Players.Ids()).Start(game.Cards.ToResponses(), game.PlayersTurn.ToResponse());
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var game = _gameManager.GetByUserId(_connectionId);
        if (game == null) return;

        _logger.LogInformation("user left game {@Id}", game.Id);

        if (game.Players.Count <= 1)
        {
            _gameManager.Remove(game.Id);
            return;
        }

        var player = game.Players.First(p => p.Id == _connectionId);
        game.RemovePlayer(player);
        await Clients.Clients(game.Players.Ids()).PlayerLeft(player.Name);
    }
}
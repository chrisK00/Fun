using Microsoft.AspNetCore.SignalR;

namespace MemorySignal.Client.Pages;

public partial class MemoryGame : IAsyncDisposable
{
    private string _winMessage;
    private bool _flipping;
    private HubConnection _connection;
    private PlayerResponse _playersTurn;
    private readonly ICollection<MemoryCardResponse> _cardsFlipped = new List<MemoryCardResponse>();
    private ICollection<PlayerResponse> _players = new List<PlayerResponse>();
    private IEnumerable<MemoryCardResponse> _cards { get; set; } = new List<MemoryCardResponse>();

    public string PlayerName { get; set; }
    public string NameModalMessage { get; set; }
    [Parameter] public string GameId { get; set; }
    [Parameter] public int CardCollectionId { get; set; }
    [Inject] NavigationManager Nav { get; set; }
    [Inject] IJSRuntime Js { get; set; }
    [Inject] IOptions<ApiOptions> ApiOptions { get; set; }

    public void Reset()
    {
        _cardsFlipped.Clear();
        _playersTurn = null;
        _winMessage = null;
        _flipping = false;
        foreach (var player in _players)
        {
            player.Points = 0;
        }
    }

    public async Task StartGame()
    {
        await _connection.SendAsync(nameof(IMemoryGameHub.Start), GameId);
    }

    public async Task OnNameSubmit(string name)
    {
        PlayerName = name;
        await JoinGame();
        await Js.InvokeVoidAsync("copyToClipboard", "invite");
    }

    public async ValueTask DisposeAsync()
    {
        if (_connection is not null) await _connection.DisposeAsync();
    }

    private void HandleAddPoint(PlayerResponse player)
    {
        player.Points++;
        if (_cards.Any(c => !c.IsFlipped)) return;

        var maxPoints = _players.Max(p => p.Points);
        var winners = _players.Where(p => p.Points == maxPoints).ToArray();

        if (winners.Length > 1) _winMessage = "It's a tie!";
        else
        {
            var winner = winners[0];
            _winMessage = winner.Name == PlayerName ? "You won!" : $"{winner.Name} won the game!";
        }
    }

    protected override async Task OnParametersSetAsync()
    {
        _connection = new HubConnectionBuilder().WithUrl(ApiOptions.Value.Url + IMemoryGameHub.Uri).Build();

        _connection.On<PlayerResponse>(nameof(IMemoryGameClient.PlayerJoined), player =>
            {
                _players.Add(player);
                StateHasChanged();
            });

        _connection.On<string, bool>(nameof(IMemoryGameClient.Turn), async (playerName, addPoint) =>
            {
                if (_cardsFlipped.Count != 2) return;
                var player = _players.First(p => p.Name == playerName);

                if (addPoint) HandleAddPoint(player);
                else
                {
                    await Task.Delay(1100);
                    foreach (var c in _cardsFlipped) c.IsFlipped = false;
                    _playersTurn = player;
                }

                _cardsFlipped.Clear();
                StateHasChanged();
            });

        _connection.On<string>(nameof(IMemoryGameClient.Flipping), id =>
            {
                var card = _cards.First(c => c.TempId == id);
                card.IsFlipped = true;
                _flipping = false;
                _cardsFlipped.Add(card);
                StateHasChanged();
            });

        _connection.On<IEnumerable<MemoryCardResponse>, PlayerResponse>(nameof(IMemoryGameClient.Start), (cards, player) =>
        {
            if (_playersTurn != null) Reset();

            _playersTurn = player;
            _cards = cards;
            StateHasChanged();
        });

        _connection.On<string, string>(nameof(IMemoryGameClient.PlayerLeft), (playerName, playersTurnName) =>
        {
            if (_playersTurn.Name == playerName)
            {
                foreach (var c in _cardsFlipped) c.IsFlipped = false;
                _cardsFlipped.Clear();
                _playersTurn = _players.First(p => p.Name == playersTurnName);
            }

            _players.Remove(_players.First(p => p.Name == playerName));
            StateHasChanged();
        });

        await _connection.StartAsync();
        if (!string.IsNullOrWhiteSpace(PlayerName)) await JoinGame();
    }

    private async Task Flip(MemoryCardResponse card)
    {
        if (_flipping || _playersTurn.Name != PlayerName || card.IsFlipped || _cardsFlipped.Any(c => c.TempId == card.TempId))
        {
            return;
        }

        _flipping = true;
        await _connection.SendAsync(nameof(IMemoryGameHub.Flip), GameId, card.TempId);
    }

    private async Task JoinGame()
    {
        try
        {
            _players = await _connection.InvokeAsync<ICollection<PlayerResponse>>(nameof(IMemoryGameHub.Join), GameId, CardCollectionId, PlayerName);
        }
        catch (HubException ex)
        {
            NameModalMessage = "The game has already started or the username is taken";
            PlayerName = null;
        }
    }
}
﻿using MemorySignal.Shared.Responses;

namespace MemorySignal.Shared.Interfaces;

public interface IMemoryGameClient
{
    /// <summary>
    /// When a new player takes over the turn
    /// </summary>
    /// <param name="playerName"></param>
    /// <param name="addPoint">If the player got a point</param>
    Task Turn(string playerName, bool addPoint);

    Task Flipping(string cardId);

    /// <summary>
    /// The game has started and both the cards and players turn are returned
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="playersTurn"></param>
    /// <returns>The cards for the game and whoose turn it is</returns>
    Task Start(IEnumerable<MemoryCardResponse> cards, PlayerResponse playersTurn);

    Task PlayerJoined(PlayerResponse player);

    /// <summary>
    /// The player who left and who's turn it is now
    /// </summary>
    /// <param name="playerName"></param>
    /// <param name="playersTurn"></param>
    Task PlayerLeft(string playerName, string playersTurnName);
}
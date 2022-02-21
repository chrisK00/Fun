﻿namespace MemorySignal.Core.Extensions;

public static class CardResponseExtensions
{
    public static CardResponse Copy(this CardResponse card) => new(card.Id, card.ImageUrl);
}

﻿using Monxterz.StatePlatform;
using Monxterz.StatePlatform.Client;
using static Monxterz.SquadRpg.MauiClient.MauiProgram;

namespace Monxterz.SquadRpg.MauiClient;

public class CharacterViewModel
{
    public GameEntityState Entity { get; }

    public CharacterViewModel(GameEntityState entity)
    {
        Entity = entity ?? throw new ArgumentNullException(nameof(entity));
    }

    public string Id => $"({Entity.Id})";
    public string DisplayName => Entity.DisplayName;
    public int Hp => Entity.GetPublicValue<int>(GameMasterId, "hp");
    public int Strength => Entity.GetPublicValue<int>(GameMasterId, "strength");

}
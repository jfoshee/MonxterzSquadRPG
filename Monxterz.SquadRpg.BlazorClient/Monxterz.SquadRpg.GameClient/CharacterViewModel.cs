using Monxterz.StatePlatform;
using Monxterz.StatePlatform.Client;
using static Monxterz.SquadRpg.GameClient.Constants;

namespace Monxterz.SquadRpg.GameClient;

public class CharacterViewModel
{
    public GameEntityState Entity { get; set; }

    public CharacterViewModel(GameEntityState entity)
    {
        Entity = entity ?? throw new ArgumentNullException(nameof(entity));
    }

    public string Id => Entity.Id!;
    public string? DisplayName => Entity.DisplayName;
    public int Hp => Entity.GetPublicValue<int>(GameMasterId, "hp");
    public int Strength => Entity.GetPublicValue<int>(GameMasterId, "strength");
    public string Stats => $"Strength: {Strength}  HP: {Hp}";
    public bool IsTraining => Entity.GetPublicValue<bool>(GameMasterId, "isTraining");
    public bool IsRecovering => Entity.GetPublicValue<bool>(GameMasterId, "isRecovering");
    public bool IsDead => Hp == 0;
    public bool ShowSpinner => !IsDead && (IsTraining || IsRecovering);
    public string OwnerName => Entity.SystemState.OwnerId;
}

using Monxterz.StatePlatform;
using Monxterz.StatePlatform.Client;
using static Monxterz.SquadRpg.GameClient.Constants;

namespace Monxterz.SquadRpg.GameClient;

public class CharacterViewModel
{
    public GameEntityState Entity { get; set; }
    private readonly Func<string?, GameEntityState?> lookupEntity;

    public CharacterViewModel(GameEntityState entity, Func<string?, GameEntityState?> lookupEntity)
    {
        Entity = entity ?? throw new ArgumentNullException(nameof(entity));
        this.lookupEntity = lookupEntity ?? throw new ArgumentNullException(nameof(lookupEntity));
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
    private string OwnerId => Entity.SystemState.OwnerId;
    public string OwnerName => lookupEntity(OwnerId)?.DisplayName;
    private string? AttackedById => Entity.GetPublicValue<string>(GameMasterId, "attackedById");
    public string? AttackedBy => lookupEntity(AttackedById)?.DisplayName;
}

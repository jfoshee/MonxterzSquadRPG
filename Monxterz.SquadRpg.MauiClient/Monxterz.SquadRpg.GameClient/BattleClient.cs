using Monxterz.StatePlatform;
using Monxterz.StatePlatform.Client;
using static Monxterz.SquadRpg.GameClient.Constants;

namespace Monxterz.SquadRpg.GameClient;

public class BattleClient
{
    public static bool IsCharacter(GameEntityState entity)
    {
        var type = entity.GetPublicValue<string>(GameMasterId, "type");
        return type == "Character";
    }
}

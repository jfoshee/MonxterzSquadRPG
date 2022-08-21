namespace Monxterz.SquadRpg.GameState.Tests;

public class AttackTest
{
    [Theory(DisplayName = "Basic"), RpgTest]
    public async Task BasicAttack(IGameTestHarness game)
    {
        GameEntityState attacker = await game.Create.Character();
        GameEntityState defender = await game.Create.Character();
        // Initialize attacker strength & defender hp
        game.State(attacker).strength = 13;
        game.State(defender).hp = 17;

        await game.Call.Attack(attacker, defender);

        // Enemy's hp should be reduced by the attacker's strength
        Assert.Equal(4, game.State(defender).hp);
    }

    // TODO: Ensure in same battle
    // TODO: Handle target(s) not passed
    // TODO: Handle not your turn
    // TODO: Handle not your character
    // TODO: Handle if character killed
}

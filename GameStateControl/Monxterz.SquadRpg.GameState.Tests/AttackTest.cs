namespace Monxterz.SquadRpg.GameState.Tests;

public class AttackTest
{
    [Theory, GameTest("monxterz-squad-rpg")]
    public async Task BasicAttack(IGameTestHarness game)
    {
        // TODO: Initialize attacker strength
        GameEntityState attacker = await game.Create.Character();
        GameEntityState enemy = await game.Create.Character();

        await game.Call.Attack(attacker, enemy);

        // Enemy's hp should be reduced by the attacker's strength
        Assert.Equal(99, game.State(enemy).hp);
    }

    // TODO: Ensure in same battle
    // TODO: Handle target(s) not passed
    // TODO: Handle not your turn
    // TODO: Handle not your character
}

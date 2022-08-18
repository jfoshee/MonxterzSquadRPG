namespace Monxterz.SquadRpg.GameState.Tests;

public class AttackTest
{
    [Theory, GameTest("monxterz-squad-rpg")]
    public async Task BasicAttack(IGameTestHarness game)
    {
        // TODO: Initialize attacker strength
        GameEntityState attacker = await game.Create.Character();
        GameEntityState enemy = await game.Create.Character();

        await game.Call.Attack(attacker, enemy.Id);

        // Enemy's hp should be reduced by the attacker's strength
        Assert.Equal(5, game.State(enemy).hp);
    }
}
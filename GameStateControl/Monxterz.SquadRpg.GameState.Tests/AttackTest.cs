namespace Monxterz.SquadRpg.GameState.Tests;

public class AttackTest
{
    [Theory, GameTest("monxterz-squad-rpg")]
    public async Task BasicAttack(IGameTestHarness game)
    {
        // TODO: Want to create these entities using the game's custom creation func
        var attacker = await game.NewEntity();
        var enemy = await game.NewEntity();
        await game.Call().Attack(attacker, enemy.Id);
        // Enemy's hp should be reduced by the attacker's strength
        Assert.Equal(5, game.State(enemy).hp);
    }
}
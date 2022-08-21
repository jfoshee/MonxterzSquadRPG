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

    [Theory(DisplayName = "Missing Attacked & Defender"), RpgTest]
    public async Task MissingAttacker(IGameTestHarness game)
    {
        await game.Invoking(async g => await (Task)g.Call.Attack())
                  .Should()
                  .ThrowAsync<ApiException>()
                  .WithMessage("*attacker*");
    }

    [Theory(DisplayName = "Missing Defender"), RpgTest]
    public async Task MissingDefender(IGameTestHarness game)
    {
        GameEntityState attacker = await game.Create.Character();

        await game.Invoking(async g => await (Task)g.Call.Attack(attacker))
                  .Should()
                  .ThrowAsync<ApiException>()
                  .WithMessage("*defender*");
    }

    [Theory(DisplayName = "Kill"), RpgTest]
    public async Task Kill(IGameTestHarness game)
    {
        GameEntityState attacker = await game.Create.Character();
        GameEntityState defender = await game.Create.Character();
        // Initialize attacker strength & defender hp
        game.State(attacker).strength = 20;
        game.State(defender).hp = 17;

        await game.Call.Attack(attacker, defender);

        // Enemy's hp should be reduced by the attacker's strength
        Assert.Equal(0, game.State(defender).hp);
    }

    [Theory(DisplayName = "Cannot Kill after Death"), RpgTest]
    public async Task CannotKillPostDeath(IGameTestHarness game)
    {
        GameEntityState attacker = await game.Create.Character();
        GameEntityState defender = await game.Create.Character();
        // Initialize attacker strength & defender hp
        game.State(attacker).hp = 0;
        game.State(attacker).strength = 20;
        game.State(defender).hp = 25;

        await game.Invoking(async g => await (Task)g.Call.Attack(attacker, defender))
                  .Should()
                  .ThrowAsync<ApiException>()
                  .WithMessage("*cannot attack*dead*");

        // Enemy's hp not should be reduced 
        Assert.Equal(25, game.State(defender).hp);
    }

    // TODO: Handle not your character
    // TODO: Ensure in same battle
    // TODO: Handle not your turn
}

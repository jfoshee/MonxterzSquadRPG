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

    [Theory(DisplayName = "Cannot attack w/ another player's character"), RpgTest]
    public async Task AnotherPlayersCharacter(IGameTestHarness game)
    {
        GameEntityState attacker = await game.Create.Character();
        GameEntityState defender = await game.Create.Character();
        await game.NewCurrentPlayer();

        await game.Invoking(async g => await (Task)g.Call.Attack(attacker, defender))
                  .Should()
                  .ThrowAsync<ApiException>()
                  .WithMessage("*cannot attack*player*");
    }

    [Theory(DisplayName = "Recovering"), RpgTest]
    public async Task Recovering(IGameTestHarness game)
    {
        GameEntityState attacker = await game.Create.Character();
        GameEntityState defender = await game.Create.Character();
        // Initially nobody is recovering
        Assert.False(game.State(attacker).isRecovering);
        Assert.False(game.State(defender).isRecovering);

        await game.Call.Attack(attacker, defender);

        // After attack Attacker is recovering
        Assert.True(game.State(attacker).isRecovering);
        Assert.False(game.State(defender).isRecovering);        
        await game.Invoking(async g => await (Task)g.Call.Attack(attacker, defender))
                  .Should()
                  .ThrowAsync<ApiException>()
                  .WithMessage("*cannot attack*recovering*");
    }

    [Theory(DisplayName = "Recovered"), RpgTest]
    public async Task Recovered(IGameTestHarness game)
    {
        GameEntityState attacker = await game.Create.Character();
        GameEntityState defender = await game.Create.Character();
        Assert.Equal(10, game.State(attacker).recoveryTime);
        game.State(attacker).recoveryTime = 2;

        await game.Call.Attack(attacker, defender);
        Assert.True(game.State(attacker).isRecovering);

        await Task.Delay(2_000);
        await game.Call.CheckStatus(attacker);

        Assert.False(game.State(attacker).isRecovering);
        await game.Call.Attack(attacker, defender);
    }

    // TODO: Ensure in same battle
    // TODO: Handle not your turn
}

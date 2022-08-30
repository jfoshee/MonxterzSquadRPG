namespace Monxterz.SquadRpg.GameState.Tests;

public class TrainingTest
{
    [Theory(DisplayName = "Start 'strength' training"), RpgTest]
    public async Task StartStrength(IGameTestHarness game)
    {
        GameEntityState trainee = await game.Create.Character();
        var expectedStart = DateTimeOffset.Now.ToUnixTimeSeconds();
        var expectedEnd = expectedStart + 30;

        await game.Call.Train(trainee, "strength", 30);
        
        Assert.True(game.State(trainee).isTraining);
        Assert.Equal("strength", game.State(trainee).trainingAttribute);
        Assert.Equal(expectedStart, game.State(trainee).trainingStart);
        Assert.Equal(expectedEnd, game.State(trainee).trainingEnd);
    }

}

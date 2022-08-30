namespace Monxterz.SquadRpg.GameState.Tests;

public class TrainingTest
{
    [Theory(DisplayName = "Start 'strength' training"), RpgTest]
    public async Task StartStrength(IGameTestHarness game)
    {
        GameEntityState trainee = await game.Create.Character();
        Assert.False(game.State(trainee).isTraining);
        var expectedStart = DateTimeOffset.Now.ToUnixTimeSeconds();
        var expectedEnd = expectedStart + 30;

        await game.Call.Train(trainee, "strength", 30);
        
        Assert.True(game.State(trainee).isTraining);
        Assert.Equal("strength", game.State(trainee).trainingAttribute);
        AssertClose(expectedStart, game.State(trainee).trainingStart);
        AssertClose(expectedEnd, game.State(trainee).trainingEnd);
    }

    void AssertClose(long expected, dynamic actual)
    {
        var actualValue = Convert.ToInt64(actual);
        Assert.InRange(actualValue, expected - 1, expected + 1);
    }
}

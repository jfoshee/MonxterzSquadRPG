namespace Monxterz.SquadRpg.GameState.Tests;

public class CharacterTest
{
    [Theory(DisplayName = "Default"), RpgTest]
    public async Task DefaultCharacter(IGameTestHarness game)
    {
        GameEntityState character = await game.Create.Character();
        var characterState = game.State(character);
        Assert.Equal("Character", characterState.type);
        Assert.Equal(100, characterState.hp);
        Assert.Equal(0, characterState.xp);
        Assert.Equal(1, characterState.strength);
        Assert.Equal(10, characterState.recoveryTime);
        Assert.False(characterState.isTraining);
        Assert.False(characterState.isRecovering);
    }
}

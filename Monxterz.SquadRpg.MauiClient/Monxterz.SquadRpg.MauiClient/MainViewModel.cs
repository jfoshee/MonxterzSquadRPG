using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Monxterz.StatePlatform;
using Monxterz.StatePlatform.Client;
using Monxterz.StatePlatform.ClientServices;
using System.Windows.Input;
using static Monxterz.SquadRpg.MauiClient.MauiProgram;

namespace Monxterz.SquadRpg.MauiClient;

public class MainViewModel : ObservableObject
{
    private readonly IGameStateClient gameStateClient;

    public MainViewModel(IGameStateClient gameStateClient)
    {
        this.gameStateClient = gameStateClient;
        var getUserTask = gameStateClient.GetUserAsync();
        getUserTask.ContinueWith(task =>
        {
            user = task.Result;
            GreetingText = $"Hello, {user.DisplayName}!";
            UpdateOwnedCharacters();
            UpdateEnemyCharacters();
        });
        gameStateClient.GetEntitiesNearbyAsync().ContinueWith(task =>
        {
            allNearbyCharacters = task.Result.Where(IsCharacter).ToList();
            UpdateOwnedCharacters();
            UpdateEnemyCharacters();
        });
    }

    private GameEntityState user;
    private List<GameEntityState> allNearbyCharacters = new();
    private List<GameEntityState> ownedCharacters = new();

    private string greetingText = "Hello, World...";

    public string GreetingText
    {
        get => greetingText;
        set => SetProperty(ref greetingText, value);
    }

    private List<string> friendlyNames = new();
    public List<string> FriendlyNames
    {
        get => friendlyNames;
        set => SetProperty(ref friendlyNames, value);
    }

    private List<string> enemyNames = new();
    public List<string> EnemyNames
    {
        get => enemyNames;
        set => SetProperty(ref enemyNames, value);
    }

    private void UpdateOwnedCharacters()
    {
        if (user is not null && allNearbyCharacters.Any())
        {
            var userId = user.Id;
            ownedCharacters = allNearbyCharacters.Where(e => e.SystemState.OwnerId == userId).ToList();
            FriendlyNames = ownedCharacters.Select(DisplayName).ToList();
        }
    }

    private void UpdateEnemyCharacters()
    {
        if (user is not null && allNearbyCharacters.Any())
        {
            var userId = user.Id;
            var nonOwnedCharacters = allNearbyCharacters.Where(e => e.SystemState.OwnerId != userId).ToList();
            EnemyNames = nonOwnedCharacters.Select(DisplayName).ToList();
        }
    }

    private bool IsCharacter(GameEntityState entity)
    {
        var type = entity.GetPublicValue<string>(GameMasterId, "type");
        return type == "Character";
    }

    private string DisplayName(GameEntityState entity)
    {
        var hp = entity.GetPublicValue<int>(GameMasterId, "hp");
        return $"{entity.DisplayName} ({entity.Id})  HP: {hp}  Owner: {entity.SystemState.OwnerId}";
    }
}

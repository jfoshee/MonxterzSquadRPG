using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Monxterz.StatePlatform;
using Monxterz.StatePlatform.Client;
using Monxterz.StatePlatform.ClientServices;
using System.Windows.Input;

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
            var user = task.Result;
            GreetingText = $"Hello, {user.DisplayName}!";
        });
        gameStateClient.GetEntitiesNearbyAsync().ContinueWith(task =>
        {
            allNearbyCharacters = task.Result.Where(IsCharacter).ToList();
            EnemyNames = allNearbyCharacters.Select(DisplayName).ToList();
        });
        gameStateClient.GetEntitiesOwnedAsync().ContinueWith(task =>
        {
            ownedCharacters = task.Result.Where(IsCharacter).ToList();
            FriendlyNames = ownedCharacters.Select(DisplayName).ToList();
        });
    }

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

    private bool IsCharacter(GameEntityState entity)
    {
        var type = entity.GetPublicValue<string>("monxterz-squad-rpg", "type");
        return type == "Character";
    }

    private string DisplayName(GameEntityState entity)
    {
        return $"{entity.DisplayName} ({entity.Id})  Owner: {entity.SystemState.OwnerId}";
    }
}

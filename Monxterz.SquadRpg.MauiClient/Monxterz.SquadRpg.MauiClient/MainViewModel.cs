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
            var entities = task.Result;
            EntityNames = entities.Where(IsCharacter)
                                  .Select(e => e.DisplayName ?? e.Id)
                                  .ToList();
        });
        //EntityNames = Enumerable.Repeat("Test", 100).ToList();
    }

    private string greetingText = "Hello, World...";
    public string GreetingText
    {
        get => greetingText;
        set => SetProperty(ref greetingText, value);
    }

    private List<string> entityNames = new();
    public List<string> EntityNames
    {
        get => entityNames;
        set => SetProperty(ref entityNames, value);
    }

    private bool IsCharacter(GameEntityState entity)
    {
        var type = entity.GetPublicValue<string>("monxterz-squad-rpg", "type");
        return type == "Character";
    }
}

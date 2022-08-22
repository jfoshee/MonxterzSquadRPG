using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Monxterz.StatePlatform.Client;
using System.Windows.Input;

namespace Monxterz.SquadRpg.MauiClient;

public class MainViewModel : ObservableObject
{
    private readonly IGameStateClient gameStateClient;

    public MainViewModel(IGameStateClient gameStateClient)
    {
        IncrementCounterCommand = new RelayCommand(IncrementCounter);
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
            EntityNames = entities.Select(e => e.DisplayName ?? e.Id)
                                  .ToList();
        });
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

    private int counter;
    public int Counter
    {
        get => counter;
        private set => SetProperty(ref counter, value);
    }

    public ICommand IncrementCounterCommand { get; }

    private void IncrementCounter()
    {
        Counter++;
        ButtonText = $"Clicked {Counter} times";
    }

    private string buttonText = "Click Me";
    public string ButtonText
    {
        get => buttonText;
        set => SetProperty(ref buttonText, value);
    }
}

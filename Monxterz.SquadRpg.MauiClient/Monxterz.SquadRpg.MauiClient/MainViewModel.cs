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
        getUserTask.ContinueWith(t =>
        {
            var user = t.Result;
            GreetingText = $"Hello, {user.DisplayName}!";
        });
    }

    private string greetingText = "Hello, World...";
    public string GreetingText
    {
        get => greetingText;
        set => SetProperty(ref greetingText, value);
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

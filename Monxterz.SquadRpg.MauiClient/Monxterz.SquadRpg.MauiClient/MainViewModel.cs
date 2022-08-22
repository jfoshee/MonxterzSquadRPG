using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace Monxterz.SquadRpg.MauiClient;

public class MainViewModel : ObservableObject
{
    public MainViewModel()
    {
        IncrementCounterCommand = new RelayCommand(IncrementCounter);
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

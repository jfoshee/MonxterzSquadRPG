using Monxterz.StatePlatform.Client;

namespace Monxterz.SquadRpg.MauiClient;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		// HACK: Get service here so that exceptions can be observed in the debugger
		//var gameStateClient = sp.GetRequiredService<IGameStateClient>();
		//var user = gameStateClient.GetUser();
		//GreetingLabel.Text = $"Hello, {user.DisplayName}";
	}
}

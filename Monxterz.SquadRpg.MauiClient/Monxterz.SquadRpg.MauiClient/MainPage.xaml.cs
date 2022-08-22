using Monxterz.StatePlatform.Client;

namespace Monxterz.SquadRpg.MauiClient;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage(IServiceProvider sp)
	{
		InitializeComponent();
		// HACK: Get service here so that exceptions can be observed in the debugger
		var gameStateClient = sp.GetRequiredService<IGameStateClient>();
		var user = gameStateClient.GetUser();
		GreetingLabel.Text = $"Hello, {user.DisplayName}";
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";
		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

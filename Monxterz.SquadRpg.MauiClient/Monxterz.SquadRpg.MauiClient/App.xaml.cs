namespace Monxterz.SquadRpg.MauiClient;

public partial class App : Application
{
	public App(AppShell appShell)
	{
		InitializeComponent();
		MainPage = appShell;
	}
}

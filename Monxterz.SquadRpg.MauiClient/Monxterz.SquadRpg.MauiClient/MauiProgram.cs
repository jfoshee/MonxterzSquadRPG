namespace Monxterz.SquadRpg.MauiClient;

public static class MauiProgram
{
	public const string GameMasterId = "monxterz-squad-rpg";

	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		builder.Services.AddSingleton<AppShell>()
						.AddTransient<MainPage>()
						.AddTransient<MainViewModel>()
						.AddGameStateClientServices(GameMasterId, "https://localhost:7264");
		return builder.Build();
	}
}

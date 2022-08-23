using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Monxterz.SquadRpg.GameClient;
using Monxterz.StatePlatform;
using Monxterz.StatePlatform.Client;
using Monxterz.StatePlatform.ClientServices;
using System.Diagnostics;
using System.Windows.Input;
using static Monxterz.SquadRpg.GameClient.BattleClient;

namespace Monxterz.SquadRpg.MauiClient;

public class MainViewModel : ObservableObject
{
    private readonly IGameStateClient gameStateClient;
    private readonly IGameTestHarness game;

    public MainViewModel(IGameStateClient gameStateClient, IGameTestHarness game)
    {
        this.gameStateClient = gameStateClient;
        this.game = game;
        AttackCommand = new AsyncRelayCommand(Attack);
    }

    public void Load()
    {
        var getUserTask = gameStateClient.GetUserAsync();
        getUserTask.ContinueWith(task =>
        {
            user = task.Result;
            GreetingText = $"Hello, {user.DisplayName}!";
            UpdateOwnedCharacters();
            UpdateEnemyCharacters();
            Trace.WriteLine("Set User");
        });
        gameStateClient.GetEntitiesNearbyAsync().ContinueWith(task =>
        {
            allNearbyCharacters = task.Result.Where(IsCharacter).ToList();
            UpdateOwnedCharacters();
            UpdateEnemyCharacters();
            Trace.WriteLine("Set allNearbyCharacters");
        });
    }

    private GameEntityState user;
    private List<GameEntityState> allNearbyCharacters = new();
    private List<GameEntityState> ownedCharacters = new();
    private List<GameEntityState> enemyCharacters = new();

    public IAsyncRelayCommand AttackCommand { get; }

    private string greetingText = "Hello, World...";
    public string GreetingText
    {
        get => greetingText;
        set => SetProperty(ref greetingText, value);
    }

    private List<CharacterViewModel> friendlies = new();
    public List<CharacterViewModel> Friendlies
    {
        get => friendlies;
        set => SetProperty(ref friendlies, value);
    }

    private List<CharacterViewModel> enemies = new();
    public List<CharacterViewModel> Enemies
    {
        get => enemies;
        set => SetProperty(ref enemies, value);
    }

    private CharacterViewModel selectedFriendly;
    public CharacterViewModel SelectedFriendly
    {
        get => selectedFriendly;
        set => SetProperty(ref selectedFriendly, value);
    }

    private CharacterViewModel selectedEnemy;
    public CharacterViewModel SelectedEnemy
    {
        get => selectedEnemy;
        set => SetProperty(ref selectedEnemy, value);
    }

    private async Task Attack()
    {
        await game.Call.Attack(selectedFriendly.Entity, SelectedEnemy.Entity);
        // HACK: Refresh enemies
        SelectedEnemy = new CharacterViewModel(SelectedEnemy.Entity);
        Enemies = enemyCharacters.Select(e => new CharacterViewModel(e)).ToList();
    }

    private void UpdateOwnedCharacters()
    {
        if (user is not null && allNearbyCharacters.Any())
        {
            var userId = user.Id;
            ownedCharacters = allNearbyCharacters.Where(e => e.SystemState.OwnerId == userId).ToList();
            Friendlies = ownedCharacters.Select(e => new CharacterViewModel(e)).ToList();
        }
    }

    private void UpdateEnemyCharacters()
    {
        if (user is not null && allNearbyCharacters.Any())
        {
            var userId = user.Id;
            enemyCharacters = allNearbyCharacters.Where(e => e.SystemState.OwnerId != userId).ToList();
            Enemies = enemyCharacters.Select(e => new CharacterViewModel(e)).ToList();
        }
    }
}

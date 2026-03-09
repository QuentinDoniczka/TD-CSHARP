using ConsoleQuentinDoniczka;
using ConsoleQuentinDoniczka.Data;
using ConsoleQuentinDoniczka.Data.Migrations;
using ConsoleQuentinDoniczka.Input;
using ConsoleQuentinDoniczka.Services;
using ConsoleQuentinDoniczka.UI;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

const string connectionString = "Host=localhost;Port=5433;Database=morpion;Username=morpion;Password=morpion";

RunMigrations(connectionString);

var dbContextOptions = new DbContextOptionsBuilder<GameDbContext>()
    .UseNpgsql(connectionString)
    .Options;
var dbContext = new GameDbContext(dbContextOptions);

IGameHistoryService historyService = new GameHistoryService(dbContext);
IGameSaveService saveService = new GameSaveService(dbContext);
IUserInput userInput = new UserInput();
IDisplay display = new DisplayConsole(userInput);

var playAgain = true;
while (playAgain)
{
    var stats = await historyService.GetStatsAsync();
    var morpion = new Morpion(display, stats, saveService);
    var result = await morpion.Start();
    await historyService.RecordGameAsync(result);

    display.ShowPlayAgainPrompt();
    var input = Console.ReadLine();
    playAgain = input?.Trim().ToLower() == "o";
}

static void RunMigrations(string connectionString)
{
    using var serviceProvider = new ServiceCollection()
        .AddFluentMigratorCore()
        .ConfigureRunner(rb => rb
            .AddPostgres()
            .WithGlobalConnectionString(connectionString)
            .ScanIn(typeof(CreateGameRecordsTable).Assembly).For.Migrations())
        .AddLogging(lb => lb.AddFluentMigratorConsole())
        .BuildServiceProvider(false);

    using var scope = serviceProvider.CreateScope();
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

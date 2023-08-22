// See https://aka.ms/new-console-template for more information
using CardMemorizationSimulator.Console;
using CardMemorizationSimulator.Domain;
using CardMemorizationSimulatorTests;
using Spectre.Console;

AnsiConsole.WriteLine("Hello Tarot!");
AnsiConsole.Confirm("Lancer la partie ?");

var game = new TarotGame(Deck.CreateTarotDeck());
var analyzer = new TarotGameAnalyzer();

var stateManager = new GameStateManager(game, analyzer);
stateManager.OnCardPlayed += (cardPlayed) =>
{
    // var card = Displayer.DisplayCard(cardPlayed.Card);
    // AnsiConsole.Write(card);
    // Thread.Sleep(1000);
};

stateManager.OnTurnFinished += (cardTurn) =>
{
    Thread.Sleep(2000);
    AnsiConsole.WriteLine($"fin de tour.");
    Displayer.DisplayTableTop(cardTurn);

    var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
        .Title("Continuer ou voir les stats?")
        .AddChoices(new[]
        {
            "Continuer",
            "Voir les stats"
        }));
    
    if (choice == "Voir les stats")
    {
        Displayer.DisplayStats(stateManager);
    } 
};

stateManager.Run();

AnsiConsole.WriteLine("Fin de partie!");

internal partial class Program { }

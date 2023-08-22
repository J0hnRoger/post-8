// See https://aka.ms/new-console-template for more information

using System.ComponentModel.DataAnnotations;
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
    AnsiConsole.WriteLine($"{cardPlayed}");
    Thread.Sleep(1000);
};

stateManager.OnTurnFinished += (cardTurn) =>
{
    AnsiConsole.WriteLine($"fin de tour.");
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

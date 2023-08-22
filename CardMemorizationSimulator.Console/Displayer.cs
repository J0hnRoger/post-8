using Spectre.Console;

namespace CardMemorizationSimulator.Console;

class Displayer
{
    public static void DisplayStats(GameStateManager gameManager)
    {
        var result = gameManager.Analyzer.Analyze(gameManager.Game);
        foreach (var (family, cardCount) in result.CardCounts)
            AnsiConsole.WriteLine($"{family.Name}: {cardCount.Remaining}");

        AnsiConsole.Confirm("continuer ?");
    }
}

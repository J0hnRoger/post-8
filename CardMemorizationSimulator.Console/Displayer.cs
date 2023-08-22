using CardMemorizationSimulator.Domain;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace CardMemorizationSimulator.Console;

class Displayer
{
    public static void DisplayTableTop(CardTurn turn)
    {
        Layout tableTop = new Layout("Root")
            .SplitColumns(
                new Layout("UltraLeft").Size(15)
                    .SplitRows(
                        // new Layout("TopRow"),
                        new Layout("MiddleRow")
                        // new Layout("BottomRow")
                    ),
                new Layout("MiddleLeft").Size(15)
                    .SplitRows(
                        new Layout("TopRow2")
                        // new Layout("MiddleRow2")
                        // new Layout("BottomRow2")
                    ),
                new Layout("MiddleRight").Size(15)
                    .SplitRows(
                        new Layout("TopRow3"),
                        // new Layout("MiddleRow3"),
                        new Layout("BottomRow3")
                    ),
                new Layout("UltraRight").Size(15)
                    .SplitRows(
                        // new Layout("TopRow4"),
                        new Layout("MiddleRow4")
                        // new Layout("BottomRow4")
                    )
            );
        
        tableTop["TopRow2"].Update(
            Align.Center(
                DisplayCard(turn.PlayedCards.Pop().Card),
                VerticalAlignment.Top
            )
        );

        tableTop["TopRow3"].Update(
            Align.Center(
                DisplayCard(turn.PlayedCards.Pop().Card),
                VerticalAlignment.Top
            )
        );
        
        tableTop["MiddleRow4"].Update(
            Align.Center(
                DisplayCard(turn.PlayedCards.Pop().Card),
                VerticalAlignment.Middle
            )
        );
        
        tableTop["BottomRow3"].Update(
            Align.Left(
                DisplayCard(turn.PlayedCards.Pop().Card),
                VerticalAlignment.Middle
            )
        );
        
        tableTop["MiddleRow"].Update(
            Align.Center(
                DisplayCard(turn.PlayedCards.Pop().Card),
                VerticalAlignment.Middle
            )
        );
        AnsiConsole.Write(tableTop);
    }

    public static Panel DisplayCard(Card card)
    {
        // affect Color for each family
        Color color = Color.White;
        if(card.Family == CardFamily.Heart)
            color = Color.Red;
        if(card.Family == CardFamily.Diamond)
            color = Color.Red3;
        if(card.Family == CardFamily.Club)
            color = Color.Green3;
        if(card.Family == CardFamily.Spade)
            color = Color.Wheat1;
        if(card.Family == CardFamily.Atout)
            color = Color.Silver;

        var cardPanel = new Panel($"[bold] {card.Value.Name} [/]")
            .Header(card.Family.DisplayName, Justify.Center)
            .Padding(2, 2, 2, 2)
            .Border(new RoundedBoxBorder())
            .BorderColor(color);
        cardPanel.Width = 8;
        return cardPanel;
    }
    
    public static void DisplayStats(GameStateManager gameManager)
    {
        var result = gameManager.Analyzer.Analyze(gameManager.Game);
        foreach (var (family, cardCount) in result.CardCounts)
            AnsiConsole.WriteLine($"{family.Name}: {cardCount.Remaining}");

        AnsiConsole.Confirm("continuer ?");
    }
}

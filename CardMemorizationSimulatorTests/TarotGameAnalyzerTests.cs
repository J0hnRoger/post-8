using CardMemorizationSimulator.Domain;

namespace CardMemorizationSimulatorTests;

public class TarotGameAnalyzerTests
{

    [Fact]
    public void TartoGameAnalyzer_Init_CradCounts()
    {
        var game = new TarotGame(Deck.CreateTarotDeck());
        var analyzer = new TarotGameAnalyzer();
        var result = analyzer.Analyze(game);
        result.Should().NotBeNull();
        result.CardCounts.Should().HaveCount(5);
        
        var atoutCount = result.CardCounts[CardFamily.Atout];
        atoutCount.Remaining = 0;
        atoutCount.Fallen.Count().Should().Be(0);
        atoutCount.Cut.Count().Should().Be(0);
        
        var spadeCount = result.CardCounts[CardFamily.Spade];
        spadeCount.Remaining = 0;
        spadeCount.Fallen.Count().Should().Be(0);
        spadeCount.Cut.Count().Should().Be(0);
    }
    
    [Fact]
    public void TartoGameAnalyzer_GetGameStats_AfterTwoTurn()
    {
        TestDeck testDeck = new TestDeck(Deck.CreateTarotDeck().Cards);
        var game = new TarotGame(testDeck);
        
        var analyzer = new TarotGameAnalyzer();
        game.Start();
        
        game.PlayTurn();
        game.PlayTurn();
        
        var result = analyzer.Analyze(game);
        
        var atoutCount = result.CardCounts[CardFamily.Heart];
        atoutCount.Remaining = 4;
        atoutCount.Fallen.Count().Should().Be(10);
        atoutCount.Cut.Count().Should().Be(0);
    }
}

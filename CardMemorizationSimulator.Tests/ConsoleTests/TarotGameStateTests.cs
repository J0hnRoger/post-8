using CardMemorizationSimulator.Console;
using CardMemorizationSimulator.Domain;

namespace CardMemorizationSimulatorTests.ConsoleTests;

public class TarotGameStateTests    
{
    [Fact] 
    public void TarotGameStateManager_LaunchAGame()
    {
        TestDeck testDeck = new(Deck.CreateTarotDeck().Cards);
        var game = new TarotGame(testDeck);
        var analyzer = new TarotGameAnalyzer();
        
        var gameStateManager = new GameStateManager(game, analyzer);
        int onCardPlayedNbCall = 0;
        gameStateManager.OnCardPlayed += (cardPlayed) =>
        {
            cardPlayed.Should().NotBeNull();
            onCardPlayedNbCall++; 
        };
        
        int onTurnFinishedNbCall = 0;
        gameStateManager.OnTurnFinished += (turn) =>
        {
            turn.Should().NotBeNull();
            onTurnFinishedNbCall++;
        };
       
        gameStateManager.Run();
        
        onCardPlayedNbCall.Should().Be(75);
        onTurnFinishedNbCall.Should().Be(15);
    }
}
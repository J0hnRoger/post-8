namespace CardMemorizationSimulatorTests;

public class DeckTests
{

    [Fact]
    public void Deck_Contains78Cards()
    {
       Deck.CreateTarotDeck()
           .Should().HaveCount(78);     
    }
    
    [Fact]
    public void Deck_Contains14CardsOfEachFamily()
    {
        
    }
    
    [Fact]
    public void Deck_Contains21Atouts()
    {
        
    }
}
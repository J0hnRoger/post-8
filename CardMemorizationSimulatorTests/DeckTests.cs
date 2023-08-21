namespace CardMemorizationSimulatorTests;

public class DeckTests
{

    [Fact]
    public void Deck_Contains78Cards()
    {
       Deck.CreateTarotDeck().Cards
           .Should().HaveCount(78);     
    }
    
    [Fact]
    public void Deck_Shuffle_ShouldRandomlyChangeCardOrders()
    {
        var deck = Deck.CreateTarotDeck();
        var firstCard = deck.Cards.First();
        
        deck.Shuffle();
        var cardAfter =  deck.Cards.First();
        
        cardAfter.Should().NotBe(firstCard);
        
        deck.Shuffle();
        var reshuffledCard = deck.Cards.First();
        reshuffledCard.Should().NotBe(cardAfter);
    }
    
    [Fact]
    public void Deck_Contains21Atouts()
    {
        
    }
}
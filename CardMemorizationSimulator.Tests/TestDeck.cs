using CardMemorizationSimulator.Domain;

namespace CardMemorizationSimulatorTests;

public class TestDeck  : Deck
{
    public TestDeck(List<Card> tarotDeck) : base(tarotDeck) { }
    
    public static new TestDeck CreateTarotDeck()
    {
        return new TestDeck(Deck.CreateTarotDeck().Cards);
    }
    
    public override void Shuffle()
    {
        List<Card> orderedCardByFamilies = Cards.GroupBy(c => c.Family)
            .Select(c => c.OrderBy(card => card.Value).ToList())
            .SelectMany(cc => cc).ToList();
        
        Cards = orderedCardByFamilies;
    }
}
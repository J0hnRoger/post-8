using CardMemorizationSimulator.Domain;

namespace CardMemorizationSimulatorTests;

public class Deck
{
    protected internal Deck(List<Card> tarotDeck)
    {
        Cards = tarotDeck;
    }

    public List<Card> Cards { get; protected set; }
   
    public static Deck CreateTarotDeck()
    {
        // Instancier 78 cartes
        List<Card> tarotDeck = new();
        foreach (var family in CardFamily.AllCardFamily)
        {
            foreach (CardValue colorValue in CardValue.FamilyCards)
            {
               tarotDeck.Add(new Card(family, colorValue)); 
            }
        }

        foreach (CardValue atout in CardValue.Atouts)
           tarotDeck.Add(new Card(CardFamily.Atout, atout));

        return new Deck(tarotDeck);
    }

    public virtual void Shuffle()
    {
       // change randomly the order of the Cards property  
       Cards = Cards.OrderBy(c => Guid.NewGuid()).ToList();
    }
}
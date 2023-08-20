using CardMemorizationSimulator.Domain;

namespace CardMemorizationSimulatorTests;

public class Deck
{
    public static List<Card> CreateTarotDeck()
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

        return tarotDeck;
    }
}
namespace CardMemorizationSimulator.Domain;

public class Player
{
    public string Name { get; private set; }
    public bool HasCardsLeft => Hand.Any();
    public List<Card> Hand { get; set; } = new();

    public Player(string name)
    {
        Name = name;
    }

    public Card OpenTurn()
    {
        var playedCard = Hand.FirstOrDefault();
        if (playedCard == null)
            throw new Exception("No more card to play");
        
        Hand.Remove(playedCard); 
        
        return playedCard;
    }

    public Card Play(Card askedCard)
    {
        if (!Hand.Any())
            throw new Exception("No more card to play");
        
        var candidateCards = Hand
            .Where(c => c.Family == askedCard.Family);
        
        var choicedCard = candidateCards.FirstOrDefault();
        
        if (choicedCard == null)
            choicedCard = Hand.FirstOrDefault(c => c.Family == CardFamily.Atout);

        if (choicedCard == null)
            choicedCard = Hand.First();
            
        Hand.Remove(choicedCard);
        
        return choicedCard;
    }

    public override string ToString()
    {
        return Name;
    }
}
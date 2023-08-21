namespace CardMemorizationSimulator.Domain;

public class TarotGameReport
{
    public Dictionary<CardFamily, CardCount> CardCounts { get; set; } = new();
}

public class CardCount
{
   public List<CardValue> Fallen { get; set; } = new(); 
   public int Remaining { get; set; }
   public List<Player> Cut { get; set; } = new List<Player>();
    
    private CardCount(List<Card> cardPlayeds)
    {
        if (cardPlayeds.Count == 0)
            return;
        
        if (cardPlayeds.GroupBy(c => c.Family).Count() > 1)
            throw new Exception("CardCount must be for one family only");
        
        int totalCards = cardPlayeds.First().Family.NbCard;
        Fallen = cardPlayeds.Select(c => c.Value).ToList(); 
        Remaining = totalCards - cardPlayeds.Count;
    }

    public static CardCount CreateNullCardCount(CardFamily family)
    {
       return new CardCount(new List<Card>())
       {
           Remaining = family.NbCard 
       };
    }
    
    public static CardCount CreateCardCount(List<CardPlayed> cardPlayeds)
    {
       return new CardCount(cardPlayeds.Select(cp => cp.Card).ToList());
    }
}

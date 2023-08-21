namespace CardMemorizationSimulator.Domain;

public class TarotGameAnalyzer
{
    public TarotGameReport Analyze(TarotGame game)
    {
        var playedCardsByFamily = game.TurnsHistory.SelectMany(ct => ct.PlayedCards)
            .GroupBy(c => c.Card.Family)
            .ToDictionary(kv => kv.Key, kv => kv.ToList());

        var report = new TarotGameReport() { };
        
        foreach (CardFamily family in CardFamily.AllCardFamily)
            report.CardCounts[family] = CardCount.CreateNullCardCount(family);
        
        report.CardCounts[CardFamily.Atout] = CardCount.CreateNullCardCount(CardFamily.Atout);
        
        foreach (KeyValuePair<CardFamily,List<CardPlayed>> playedCards in playedCardsByFamily)
        {
            report.CardCounts[playedCards.Key] = CardCount.CreateCardCount(playedCards.Value); 
        } 
        return report;
    }
}
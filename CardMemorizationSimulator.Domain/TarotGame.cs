using System.Diagnostics;

namespace CardMemorizationSimulator.Domain;

public class TarotGame
{
    public List<Player> Players { get; private set; }
    public int NbTurn { get; set; } = 0;
    public Queue<Player> CurrentTurn { get; internal set; } = new();

    public void Start()
    {
        Players = new List<Player>()
        {
            new Player(),
            new Player(),
            new Player(),
            new Player(),
            new Player(),
        };

        foreach (Player player in Players)
        {
            DistributeCardsTo(player);
        }
    }

    private void DistributeCardsTo(Player player)
    {
       for (int i = 0; i < 15; i++)
       {
           player.Hand.Add(new Card());
       } 
    }

    /// <summary>
    /// Get the next card played - in function of the turn players
    /// </summary>
    public void GetNextCard()
    {
        if (CurrentTurn.Count == 0)
        {
            NbTurn++;
            CurrentTurn = new Queue<Player>(Players);
        }

        if (Players.All(p => !p.Hand.Any()))
            throw new Exception("current game is finished.");
        
        // each player should put down a card in the order of the Players list
        var currentPlayer = CurrentTurn.Dequeue();
        currentPlayer.Play();
    }
}

public class Player
{
    public List<Card> Hand { get; set; } = new();

    public Card Play()
    {
        var playedCard = Hand.FirstOrDefault();
        if (playedCard == null)
            throw new Exception("No more card to play");
        
        Hand.Remove(playedCard); 
        
        return playedCard;
    }
}

public class Card
{
}
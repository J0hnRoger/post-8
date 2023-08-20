using CSharpFunctionalExtensions;

namespace CardMemorizationSimulator.Domain;

public class TarotGame
{
    public List<Player> Players { get; private set; }
    public int NbTurn { get; set; } = 0;
    public Queue<Player> CurrentTurn { get; internal set; } = new();
    
    public IReadOnlyList<Card> Dog { get; private set; }
    
    public bool IsFinished => NbTurn == 18;
    public bool PlayersHaveNoCardsLeft => Players.All(p => !p.HasCardsLeft);
    
    /// <summary>
    /// All cards in the game
    /// </summary>
    public IReadOnlyList<Card> Deck { get; }

    public TarotGame()
    {
        Deck = CardMemorizationSimulatorTests.Deck.CreateTarotDeck();
    }
    
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

        DistributeCards();
    }

    private void DistributeCards()
    {
        int playerIdx = 0;
        // On prend le Chien: 
        Dog = Deck.Take(3).ToList();
        var deckWithoutDog = Deck.Skip(3).ToList(); 
            
        foreach (Card card in deckWithoutDog)
        {
            if (playerIdx == Players.Count)
                playerIdx = 0; 
            
            Players[playerIdx].Hand.Add(card);
            playerIdx++;
        }
    }

    /// <summary>
    /// Get the next card played - in function of the turn players
    /// </summary>
    public Result<Card> GetNextCard()
    {
        if (CurrentTurn.Count == 0)
        {
            NbTurn++;
            CurrentTurn = new Queue<Player>(Players);
        }

        if (PlayersHaveNoCardsLeft)
            return Result.Failure<Card>("current game is finished.");
        
        // each player should put down a card in the order of the Players list
        var currentPlayer = CurrentTurn.Dequeue();
        var playedCard = currentPlayer.Play();
        return Result.Success(playedCard);
    }
}

public class Player
{
    public bool HasCardsLeft => Hand.Any();
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
    public CardFamily Family { get; private set; }
    public CardValue Value { get; private set; }
    
    public Card(CardFamily family, CardValue value)
    {
        Family = family;
        Value = value;
    }
}
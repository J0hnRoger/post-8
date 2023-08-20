using CSharpFunctionalExtensions;

namespace CardMemorizationSimulator.Domain;

public class TarotGame
{
    public List<Player> Players { get; private set; }
    public int NbTurn { get; set; } = 0;
    public Queue<Player> CurrentTurn { get; internal set; } = new();
    
    public IReadOnlyList<Card> Dog { get; private set; }
    
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
        var playedCard = currentPlayer.OpenTurn();
        return Result.Success(playedCard);
    }

    public Result<CardTurn> PlayTurn()
    {
        if (CurrentTurn.Count == 0)
        {
            NbTurn++;
            CurrentTurn = new Queue<Player>(Players);
        }
        
        if (PlayersHaveNoCardsLeft)
            return Result.Failure<CardTurn>("current game is finished.");

        Card? askedCard = null;
        CardTurn currentTurn = new CardTurn();
        while (CurrentTurn.Count > 0)
        {
            var currentPlayer = CurrentTurn.Dequeue();
            Card currentCard;
            if (askedCard == null)
            {
                askedCard = currentPlayer.OpenTurn();
                currentCard = askedCard;
            }
            else
                currentCard = currentPlayer.Play(askedCard);
            
            currentTurn.PlayedCards.Push(new CardPlayed()
            {
                Card = currentCard,
                Player = currentPlayer
            });
        }
        return Result.Success(currentTurn);
    }
}

public class CardTurn
{
    public Stack<CardPlayed> PlayedCards { get; set; }  = new Stack<CardPlayed>();
    public Player Winner { get; set; } 
    public int NbTurn;
}

public class CardPlayed
{
    public Card Card { get; set; } 
    public Player Player { get; set; } 
}

public class Player
{
    public bool HasCardsLeft => Hand.Any();
    public List<Card> Hand { get; set; } = new();

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
        // TODO - Get card with same family as askedCard 
        var playedCard = Hand.FirstOrDefault();
        if (playedCard == null)
            throw new Exception("No more card to play");
        
        Hand.Remove(playedCard); 
        
        return playedCard;
    }
}
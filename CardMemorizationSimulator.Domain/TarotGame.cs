using System.Collections.ObjectModel;
using CardMemorizationSimulatorTests;
using CSharpFunctionalExtensions;

namespace CardMemorizationSimulator.Domain;

public class TarotGame
{
    public List<Player> Players { get; private set; }
    public int NbTurns => _turnsHistory.Count;
    public Queue<Player> CurrentTurn { get; internal set; } = new();
    
    public IReadOnlyList<Card> Dog { get; private set; }
    
    public bool PlayersHaveNoCardsLeft => Players.All(p => !p.HasCardsLeft);

    private List<CardTurn> _turnsHistory = new();
    public List<CardTurn> TurnsHistory => _turnsHistory; 
    
    private CardTurn _lastCardTurn => _turnsHistory.Last(); 
    
    /// <summary>
    /// All cards in the game
    /// </summary>
    public IReadOnlyList<Card> Deck { get; }

    public TarotGame(Deck deck)
    {
        deck.Shuffle();
        Deck = deck.Cards;
    }
    
    public void Start()
    {
        Players = new List<Player>();
        for (var i = 0; i < 5; i++)
            Players.Add(new Player($"Player {i + 1}"));    

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

    public Result<CardTurn> PlayTurn()
    {
        if (CurrentTurn.Count == 0)
        {
            CurrentTurn = new Queue<Player>(Players);
        }
        else
        {
            // Re-order the Queue<Player> with the winner of the last turn in _lastCardTurn first, in the same order than before 
            var winner = _lastCardTurn.GetWinner().Value;
            var winnerIndex = Players.FindIndex(p => p == winner);
            
            var orderedPlayers = Players.Skip(winnerIndex).Concat(Players.Take(winnerIndex));
            
            CurrentTurn = new Queue<Player>(orderedPlayers);
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
            
            currentTurn.PlayedCards.Add(new CardPlayed()
            {
                Card = currentCard,
                Player = currentPlayer
            });
        }

        _turnsHistory.Add(currentTurn);
        currentTurn.NbTurn = NbTurns;
        return Result.Success(currentTurn);
    }
}

public class CardTurn
{
    public List<CardPlayed> PlayedCards { get; set; }  = new List<CardPlayed>();

    public int NbTurn;
    
    public Result<Player> GetWinner()
    {
        if (!PlayedCards.Any())
            Result.Failure($"Aucune carte jouée ce tour: {NbTurn}");
            
        var winner = PlayedCards.ToList()
            .OrderByDescending(p => p.Card.Value).First().Player;
        
        return winner;
    }
}

public class CardPlayed
{
    public Card Card { get; set; } 
    public Player Player { get; set; }
    public override string ToString()
    {
        return $"{Card} played by {Player}";
    }
}
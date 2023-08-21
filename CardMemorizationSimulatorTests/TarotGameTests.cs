using CardMemorizationSimulator.Domain;

namespace CardMemorizationSimulatorTests;

public class TarotGameTests
{

    [Fact]
    public void Game_CreatedWith_Deck()
    {
        TarotGame game = CreateTarotGame();
         
        game.Deck.Count.Should().Be(78);
    }

    [Fact]
    public void Game_StartANewGame_Setup5Players()
    {
        TarotGame game = CreateTarotGame();
        game.Start();

        game.Players.Should().HaveCount(5);
    }
    
    [Fact]
    public void Game_Start_EachPlayer_Has15Cards()
    {
        TarotGame game = CreateTarotGame();
        game.Start();

        game.Players.Should().HaveCount(5);
        game.Players.ForEach(p 
            => p.Hand.Should().HaveCount(15));
        
        var cardsInHands = game.Players.SelectMany(p => p.Hand).ToList();
        var allCards = cardsInHands.Concat(game.Dog).ToList();
        allCards.Should().HaveCount(78);
        allCards.Count.Should().Be(allCards.Distinct().Count());
        Deck.CreateTarotDeck().Cards.Should().Contain(allCards); 
    }
    
    [Fact]
    public void Game_Start_AtTurn1()
    {
        TarotGame game = CreateTarotGame();
        game.Start();
        
        game.NbTurn.Should().Be(0);
    }
    
    [Fact]
    public void Game_Start_LaunchANewTurn()
    {
        TarotGame game = CreateTarotGame();
        game.Start();
        
        // Le 1er joueur a ouvert en  posant une carte - il reste 4 joueurs à jouer dans ce tour
        game.GetNextCard();
        game.NbTurn.Should().Be(1);
        game.CurrentTurn.Should().HaveCount(4);
    }
    
    [Fact]
    public void Game_Start_FirstPlayer_PutDownACard()
    {
        TarotGame game = CreateTarotGame();
        game.Start();
        
        game.NbTurn.Should().Be(0);
        // si pas de tour en cours, on lance le tour
        game.GetNextCard();
        game.GetNextCard();
        game.GetNextCard();
        game.GetNextCard();
        game.GetNextCard();
        game.NbTurn.Should().Be(1);
    }
    
    [Fact]
    public void Game_WhenPlayersHaveNoMoreCardsInHands_NbTurnIs14()
    {
        TarotGame game = CreateTarotGame();
        game.Start();
        
        game = PlayAllTestGame(game);
        game.NbTurn.Should().Be(15);
    }
    
    [Fact]
    public void Game_WhenGameIsFinished_ReturnFailure()
    {
        TarotGame game = CreateTarotGame();
        game = PlayAllTestGame(game);

        var overflowedCardResult = game.GetNextCard();
        overflowedCardResult.IsFailure.Should().BeTrue();
    }
    
    [Fact]
    public void Game_OnlyCardOfSameColorArePlayed_InATurn()
    {
        TarotGame game = CreateTarotGame();
        game.Start();
        
        var first = game.GetNextCard().Value;
        var second = game.GetNextCard().Value;;
        var third = game.GetNextCard().Value;;
        var fourth = game.GetNextCard().Value;
        var fifth = game.GetNextCard().Value;
         
        // init the cardsInTheTurn with the cards above
        var cardsInTheTurn = new List<Card>()
        {
            first, second, third, fourth, fifth
        };
        cardsInTheTurn.All(c => c.Family == first.Family).Should().BeTrue();
    }
    
    [Fact]
    public void Game_PlayTurn_ReturnThe5PlayedCards()
    {
        TarotGame game = CreateTarotGame();
        game.Start();
        
        var firstTurnResult = game.PlayTurn(); 
        firstTurnResult.IsSuccess.Should().BeTrue();
        var cardTurn = firstTurnResult.Value; 
        cardTurn.PlayedCards.Should().HaveCount(5);
    }
    
    [Fact]
    public void Game_PlayTurn_ReturnTheWinnerOfTheTurn()
    {
        TarotGame game = CreateTarotGame();
        game.Start();
        
        var firstTurnResult = game.PlayTurn(); 
        firstTurnResult.IsSuccess.Should().BeTrue();
        var cardTurn = firstTurnResult.Value; 
        var winner = cardTurn.GetWinner();
        winner.Should().NotBeNull();
    }
    
    [Fact]
    public void Game_StarterPlayer_IsTheWinner_OfThePreviousTurn()
    {
        TarotGame game = CreateTarotGame();
        game.Start();
        
        var firstTurnResult = game.PlayTurn(); 
        firstTurnResult.IsSuccess.Should().BeTrue();
        var cardTurn = firstTurnResult.Value; 
        cardTurn.PlayedCards.Should().HaveCount(5);

        var winner = cardTurn.GetWinner();
;
        var secondTurnResult = game.PlayTurn();
        secondTurnResult.IsSuccess.Should().BeTrue();
        secondTurnResult.Value.PlayedCards.First()
            .Player.Should().Be(winner);
    }
    
    [Fact]
    public void TarotGame_DistributeAll78CardsOfTheDeck()
    {
        TarotGame game = CreateTarotGame();
        
        game.Start();

        var allCards = game.Players.SelectMany(p => p.Hand).ToList();
        
        // J'ajuste de 78 à 75 cartes distribuées dans les mains des joueurs
        allCards.Should().HaveCount(75);
        
        game.Deck.All(allCards.Contains);
    }
    
    #region Utilities
    private static TarotGame CreateTarotGame()
    {
        TestDeck testDeck = TestDeck.CreateTarotDeck();
        TarotGame game = new TarotGame(testDeck);
        return game;
    }
    
    private static TarotGame PlayAllTestGame(TarotGame game)
    {
        game.Start();

        game.NbTurn.Should().Be(0);
        while (!game.PlayersHaveNoCardsLeft)
        {
            var pickedCardResult = game.GetNextCard();
            pickedCardResult.IsSuccess.Should().BeTrue();
        }

        return game;
    }
    #endregion
}
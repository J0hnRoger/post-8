using CardMemorizationSimulator.Domain;

namespace CardMemorizationSimulatorTests;

public class TarotGameTests
{
    [Fact]
    public void Game_StartANewGame_Setup5Players()
    {
        var game = new TarotGame();
        game.Start();

        game.Players.Should().HaveCount(5);
    }
    
    [Fact]
    public void Game_Start_EachPlayer_Has18Cards()
    {
        var game = new TarotGame();
        game.Start();

        game.Players.Should().HaveCount(5);
        game.Players.ForEach(p 
            => p.Hand.Should().HaveCount(15));
    }
    
    [Fact]
    public void Game_Start_AtTurn1()
    {
        var game = new TarotGame();
        game.Start();
        
        game.NbTurn.Should().Be(0);
    }
    
    [Fact]
    public void Game_Start_LaunchANewTurn()
    {
        var game = new TarotGame();
        game.Start();
        
        // Le 1er joueur a ouvert en  posant une carte - il reste 4 joueurs à jouer dans ce tour
        game.GetNextCard();
        game.NbTurn.Should().Be(1);
        game.CurrentTurn.Should().HaveCount(4);
    }
    
    [Fact]
    public void Game_Start_FirstPlayer_PutDownACard()
    {
        var game = new TarotGame();
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
        var game = new TarotGame();
        game.Start();
        
        game.NbTurn.Should().Be(0);
        while(game.Players.Any(p => p.Hand.Count > 0))
        {
            game.GetNextCard();
        }
        game.NbTurn.Should().Be(15);
    }
    
    [Fact]
    public void Game_WhenGameIsFinished_ThrowException()
    {
        var game = new TarotGame();
        game.Start();
        
        game.NbTurn.Should().Be(0);
        while(game.Players.Any(p => p.Hand.Count > 0))
        {
            game.GetNextCard();
        }

        var action = () => game.GetNextCard();
        action.Should()
            .Throw<Exception>().WithMessage("current game is finished."); 
    }
}
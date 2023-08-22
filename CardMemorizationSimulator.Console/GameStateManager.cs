using CardMemorizationSimulator.Domain;

namespace CardMemorizationSimulator.Console;

public class GameStateManager
{
    private readonly TarotGame _game;
    private readonly TarotGameAnalyzer _gameAnalyzer;

    public TarotGame Game => _game;
    public TarotGameAnalyzer Analyzer => _gameAnalyzer;
    
    public Action<CardPlayed> OnCardPlayed { get; set; }
    public Action<CardTurn> OnTurnFinished { get; set; }
    
    public GameStateManager(TarotGame game, TarotGameAnalyzer tarotGameAnalyzer)
    {
        _game = game;
        _gameAnalyzer = tarotGameAnalyzer;
        
        _game.Start();
    }

    // Game loop 
    public void Run()
    {
        if(_game.PlayersHaveNoCardsLeft)
            throw new ArgumentException("The game is already finished");
        
        while (!_game.PlayersHaveNoCardsLeft)
        {
            var turn = _game.PlayTurn();
            foreach (var playedCard in turn.Value.PlayedCards)
            {
                OnCardPlayed?.Invoke(playedCard);
            }
            OnTurnFinished?.Invoke(turn.Value);
        }
    }
}
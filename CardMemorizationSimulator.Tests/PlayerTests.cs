using CardMemorizationSimulator.Domain;

namespace CardMemorizationSimulatorTests;

public class PlayerTests
{
    [Fact]
    public void Player_PlaySameFamily_WhenHeCan()
    {
        var hand = new List<Card>()
        {
            new Card(CardFamily.Diamond, CardValue.Five),
            new Card(CardFamily.Atout, CardValue.Seven),
            new Card(CardFamily.Spade, CardValue.One),
        };
        
        Player player = CreatePlayerWithHand(hand);

        var askedCard = new Card(CardFamily.Spade, CardValue.Jack);

        Card playedCard = player.Play(askedCard);
        playedCard.Family.Should().Be(CardFamily.Spade);
    }
    
    [Fact]
    public void Player_PlayAtout_WhenHeDontHaveSameFamily()
    {
        Player player = CreatePlayerWithHand();

        var askedCard = new Card(CardFamily.Spade, CardValue.Jack);

        Card playedCard = player.Play(askedCard);
        playedCard.Family.Should().Be(CardFamily.Atout);
    }

    [Fact]
    public void Player_PlayRandomCard_WhenHeDontHaveAtout()
    {
        
        var smallHand = new List<Card>() {
            new Card(CardFamily.Spade, CardValue.Five),
            new Card(CardFamily.Club, CardValue.Seven), 
            new Card(CardFamily.Club, CardValue.One),
        };
        
        Player player = CreatePlayerWithHand(smallHand);

        var askedCard = new Card(CardFamily.Diamond, CardValue.King);

        Card playedCard = player.Play(askedCard);
        playedCard.Family.Should().Be(CardFamily.Spade);
    }
    
    private static Player CreatePlayerWithHand(List<Card>? hand = null)
    {
        var handOrDefault = hand ?? new List<Card>()
        {
            new Card(CardFamily.Diamond, CardValue.Five),
            new Card(CardFamily.Atout, CardValue.Seven), 
            new Card(CardFamily.Club, CardValue.One),
        };
        
        var player = new Player("Test player");
        player.Hand.AddRange(handOrDefault);
        return player;
    }
}
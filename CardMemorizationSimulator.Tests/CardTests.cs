using CardMemorizationSimulator.Domain;

namespace CardMemorizationSimulatorTests;

public class CardTests
{
    [Fact]
    public void Card_ReturnMeaningfullName_WhenColorCard()
    {
        var card = new Card(CardFamily.Diamond, CardValue.Eight);
        
        card.ToString().Should().Be("Eight of Diamond");
    }
    
    [Fact]
    public void Card_ReturnMeaningfullName_WhenAtoutCard()
    {
        var card = new Card(CardFamily.Atout, CardValue.TwentyOne);
        
        card.ToString().Should().Be("21 of Atout");
    }
    
    [Fact]
    public void TwoCard_AreEqual__WhenTheyHaveTheSameValueAndSameFamily()
    {
        var card1 = new Card(CardFamily.Diamond, CardValue.Eight);
        var card2 = new Card(CardFamily.Diamond, CardValue.Eight);
        bool areTheSame = card1 == card2;

        areTheSame.Should().BeTrue();
    }
    
    [Fact]
    public void TwoCard_AreNotEqual__WhenTheyHaveDifferentValueOrFamily()
    {
        var card1 = new Card(CardFamily.Diamond, CardValue.Eight);
        var card2 = new Card(CardFamily.Spade, CardValue.Eight);
        bool areTheSame = card1 == card2;

        areTheSame.Should().BeFalse();
        
        var card3 = new Card(CardFamily.Atout, CardValue.One);
        var card4 = new Card(CardFamily.Atout, CardValue.Two);
        areTheSame = card3 == card4;

        areTheSame.Should().BeFalse();
    }
    
    [Fact]
    public void TwoList_ContainsSameCards_ReturnTrue()
    {
        var list1 = new List<Card>() { new Card(CardFamily.Atout, CardValue.One), new Card(CardFamily.Club, CardValue.Two) };
        var list2 = new List<Card>() { new Card(CardFamily.Club, CardValue.Two), new Card(CardFamily.Atout, CardValue.One) };
        
        bool containsSameCards = list1.All(list2.Contains); 
    
        containsSameCards.Should().BeTrue();
    }
}
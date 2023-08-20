using CardMemorizationSimulator.Domain;

namespace CardMemorizationSimulatorTests;

public class CardTests
{
    [Fact]
    public void Card_ReturnMeaningfullName()
    {
        var card = new Card(CardFamily.Diamond, CardValue.Eight);
        
        card.ToString().Should().Be("Eight of Diamond");
    }
}
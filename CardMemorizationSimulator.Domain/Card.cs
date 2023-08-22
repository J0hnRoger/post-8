using CSharpFunctionalExtensions;

namespace CardMemorizationSimulator.Domain;

public class Card : ValueObject
{
    public CardFamily Family { get; private set; }
    public CardValue Value { get; private set; }
    
    public Card(CardFamily family, CardValue value)
    {
        Family = family;
        Value = value;
    }
    
    public override string ToString()
    {
        return $"{Value.Name} de {Family.DisplayName}";
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Family;
        yield return Value;   
    }
}
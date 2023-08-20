using CSharpFunctionalExtensions;

namespace CardMemorizationSimulator.Domain;

public class CardFamily : ValueObject
{
    public static CardFamily Heart = new( "Heart");
    public static CardFamily Diamond = new( "Diamond");
    public static CardFamily Club = new( "Club");
    public static CardFamily Spade = new( "Spade");
    public static CardFamily Atout = new( "Atout");
   
    public static List<CardFamily> AllCardFamily 
        => new () { Heart, Diamond, Club, Spade };

    public string Name { get; private set; }
   
    public CardFamily(string name)
    {
        Name = name; 
    }
   
    public static CardFamily FromString(string roleString)
    {
        return AllCardFamily.Single(r => String.Equals(r.Name, roleString, StringComparison.OrdinalIgnoreCase));
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Name;
    }
}
using CSharpFunctionalExtensions;

namespace CardMemorizationSimulator.Domain;

public class CardFamily : ValueObject
{
    public static CardFamily Heart = new( "Heart", 14, "Coeur");
    public static CardFamily Diamond = new( "Diamond", 14, "Carreaux");
    public static CardFamily Club = new( "Club", 14, "Trèfle");
    public static CardFamily Spade = new( "Spade", 14, "Pique");
    public static CardFamily Atout = new( "Atout", 22, "Atout");
   
    public static List<CardFamily> AllCardFamily 
        => new () { Heart, Diamond, Club, Spade };

    public string Name { get; }
    public int NbCard { get; }
    public string DisplayName { get; }
    
    public CardFamily(string name, int nbCard, string displayName)
    {
        Name = name;
        NbCard = nbCard;
        DisplayName = displayName;
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
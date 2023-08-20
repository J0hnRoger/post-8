namespace CardMemorizationSimulator.Domain;

public class CardFamily  
{
    public static CardFamily Heart = new(0, "Heart");
    public static CardFamily Diamond = new(1, "Diamond");
    public static CardFamily Club = new(2, "Club");
    public static CardFamily Spade = new(3, "Spade");
    public static CardFamily Atout = new(4, "Atout");
   
    public static List<CardFamily> AllCardFamily 
        => new () { Heart, Diamond, Club, Spade };

    public string Name { get; private set; }
    public int Value { get; private set; }
   
    public CardFamily(int value, string name)
    {
        Name = name; 
        Value = value;
    }
   
    public static CardFamily FromString(string roleString)
    {
        return AllCardFamily.Single(r => String.Equals(r.Name, roleString, StringComparison.OrdinalIgnoreCase));
    }

    public static CardFamily FromValue(int value)
    {
        return AllCardFamily.Single(r => r.Value == value);
    }
}
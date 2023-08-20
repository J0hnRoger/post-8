namespace CardMemorizationSimulator.Domain;

public class CardValue
{
    public static CardValue One = new CardValue(1, "Un");
    public static CardValue Two = new CardValue(2, "Deux");
    public static CardValue Three = new CardValue(3, "Three");
    public static CardValue Four = new CardValue(4, "Four");
    public static CardValue Five = new CardValue(5, "Five");
    public static CardValue Six = new CardValue(6, "Six");
    public static CardValue Seven = new CardValue(7, "Seven");
    public static CardValue Eight = new CardValue(8, "Eight");
    public static CardValue Nine = new CardValue(9, "Nine");
    public static CardValue Ten = new CardValue(10, "Ten");
    public static CardValue Jack = new CardValue(11, "Valet");
    public static CardValue Knight = new CardValue(12, "Cavalier");
    public static CardValue Queen = new CardValue(13, "Dame");
    public static CardValue King = new CardValue(14, "Roi");
    // Atout
    public static CardValue Excuse = new CardValue(0, "Excuse Atout");
    public static CardValue LittleBoy = new CardValue(15, "Petit Atout");
    public static CardValue TwoAtout = new CardValue(16, "2 Atout");
    public static CardValue ThreeAtout = new CardValue(17, "3 Atout");
    public static CardValue FourAtout = new CardValue(18, "4 Atout");
    public static CardValue FiveAtout  = new CardValue(19, "5 Atout");
    public static CardValue SixAtout  = new CardValue(20, "6 Atout");
    public static CardValue SevenAtout  = new CardValue(21, "7 Atout");
    public static CardValue EightAtout  = new CardValue(22, "8 Atout");
    public static CardValue NineAtout  = new CardValue(23, "9 Atout");
    public static CardValue TenAtout  = new CardValue(24, "10 Atout");
    public static CardValue ElevenAtout  = new CardValue(25, "11 Atout");
    public static CardValue TwelveAtout  = new CardValue(26, "12 Atout");
    public static CardValue ThirteenAtout  = new CardValue(27, "13 Atout");
    public static CardValue ForteenAtout  = new CardValue(28, "14 Atout");
    public static CardValue FifteenAtout  = new CardValue(29, "15 Atout");
    public static CardValue SixteenAtout  = new CardValue(30, "16 Atout");
    public static CardValue SeventeenAtout  = new CardValue(31 ,"17 Atout");
    public static CardValue HeighteenAtout  = new CardValue(32, "18 Atout");
    public static CardValue NineteenAtout  = new CardValue(33, "19 Atout");
    public static CardValue TwentyAtout  = new CardValue(34, "20 Atout");
    public static CardValue TwentyOne = new CardValue(35, "21 Atout");

    public static List<CardValue> Atouts = new() { Excuse, LittleBoy, TwoAtout, ThreeAtout, FourAtout, FiveAtout, SixAtout, SevenAtout, EightAtout, NineAtout, TenAtout,ElevenAtout, TwelveAtout, ThirteenAtout, ForteenAtout, FifteenAtout, SixteenAtout , SeventeenAtout, HeighteenAtout, NineteenAtout, TwentyAtout, TwentyOne };
    public static List<CardValue> FamilyCards = new() { One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Knight, Queen, King };
    
    public static List<CardValue> AllCardValues 
        => new () { One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Knight, Queen, King, Excuse, LittleBoy, TwoAtout, ThreeAtout, FourAtout, FiveAtout, SixAtout, SevenAtout, EightAtout, NineAtout, TenAtout, ElevenAtout, TwelveAtout, ThirteenAtout, ForteenAtout, FifteenAtout, SixteenAtout, SeventeenAtout, HeighteenAtout, NineteenAtout, TwentyAtout, TwentyOne };
    
    private CardValue(int value, string name)
    {
        Value = value;
        Name = name;
    }

    public string Name { get; private set; }

    public int Value { get; private set; }
}
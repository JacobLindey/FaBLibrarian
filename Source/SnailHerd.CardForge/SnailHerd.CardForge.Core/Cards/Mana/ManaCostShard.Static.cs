namespace SnailHerd.CardForge.Core.Cards.Mana;


public partial class ManaCostShard
{
    /* Pure colors */
    public static readonly ManaCostShard White = Register(ManaAtom.White, "W");
    public static readonly ManaCostShard Blue  = Register(ManaAtom.Blue,  "U");
    public static readonly ManaCostShard Black = Register(ManaAtom.Black, "B");
    public static readonly ManaCostShard Red   = Register(ManaAtom.Red,   "R");
    public static readonly ManaCostShard Green = Register(ManaAtom.Green, "G");
    
    public static readonly ManaCostShard ColorsSuperposition = White | Blue | Black | Red | Green;
    
    /* Hybrid */
    public static readonly ManaCostShard WU = Register(ManaAtom.White | ManaAtom.Blue,  "W/U", "WU");
    public static readonly ManaCostShard WB = Register(ManaAtom.White | ManaAtom.Black, "W/B", "WB");
    public static readonly ManaCostShard UB = Register(ManaAtom.Blue  | ManaAtom.Black, "U/B", "UB");
    public static readonly ManaCostShard UR = Register(ManaAtom.Blue  | ManaAtom.Red,   "U/R", "UR");
    public static readonly ManaCostShard BR = Register(ManaAtom.Black | ManaAtom.Red,   "B/R", "BR");
    public static readonly ManaCostShard BG = Register(ManaAtom.Black | ManaAtom.Green, "B/G", "BG");
    public static readonly ManaCostShard RW = Register(ManaAtom.Red   | ManaAtom.White, "R/W", "RW");
    public static readonly ManaCostShard RG = Register(ManaAtom.Red   | ManaAtom.Green, "R/G", "RG");
    public static readonly ManaCostShard GW = Register(ManaAtom.Green | ManaAtom.White, "G/W", "GW");
    public static readonly ManaCostShard GU = Register(ManaAtom.Green | ManaAtom.Blue,  "G/U", "GU");
    
    /* Or 2 generic */
    public static readonly ManaCostShard W2 = Register(ManaAtom.White | ManaAtom.OrTwoGeneric, "2/W", "2W");
    public static readonly ManaCostShard U2 = Register(ManaAtom.Blue  | ManaAtom.OrTwoGeneric, "2/U", "2U");
    public static readonly ManaCostShard B2 = Register(ManaAtom.Black | ManaAtom.OrTwoGeneric, "2/B", "2B");
    public static readonly ManaCostShard R2 = Register(ManaAtom.Red   | ManaAtom.OrTwoGeneric, "2/R", "2R");
    public static readonly ManaCostShard G2 = Register(ManaAtom.Green | ManaAtom.OrTwoGeneric, "2/G", "2G");
    
    /* Phyrexian */
    public static readonly ManaCostShard WP = Register(ManaAtom.White | ManaAtom.OrTwoLife, "W/P", "WP");
    public static readonly ManaCostShard UP = Register(ManaAtom.Blue | ManaAtom.OrTwoLife, "U/P", "UP");
    public static readonly ManaCostShard BP = Register(ManaAtom.Black | ManaAtom.OrTwoLife, "B/P", "BP");
    public static readonly ManaCostShard RP = Register(ManaAtom.Red | ManaAtom.OrTwoLife, "R/P", "RP");
    public static readonly ManaCostShard GP = Register(ManaAtom.Green | ManaAtom.OrTwoLife, "G/P", "GP");

    public static readonly ManaCostShard BGP = Register(
        ManaAtom.Black | ManaAtom.Green | ManaAtom.OrTwoLife,
        "B/G/P", "BGP"
    );

    public static readonly ManaCostShard BRP = Register(
        ManaAtom.Black | ManaAtom.Red | ManaAtom.OrTwoLife,
        "B/R/P", "BRP"
    );

    public static readonly ManaCostShard GWP = Register(
        ManaAtom.Green | ManaAtom.White | ManaAtom.OrTwoLife,
        "G/W/P", "GWP"
    );

    public static readonly ManaCostShard RGP = Register(
        ManaAtom.Red | ManaAtom.Green | ManaAtom.OrTwoLife,
        "R/G/P", "RGP"
    );

    public static readonly ManaCostShard RWP = Register(
        ManaAtom.Red | ManaAtom.White | ManaAtom.OrTwoLife,
        "R/W/P", "RWP"
    );
    public static readonly ManaCostShard UBP = Register(ManaAtom.Blue | ManaAtom.Black | ManaAtom.OrTwoLife, "U/B/P", "UBP");
    public static readonly ManaCostShard URP = Register(ManaAtom.Blue | ManaAtom.Red | ManaAtom.OrTwoLife, "U/R/P", "URP");

    public static readonly ManaCostShard WBP = Register(
        ManaAtom.White | ManaAtom.Black | ManaAtom.OrTwoLife,
        "W/B/P", "WBP"
    );
    
    public static readonly ManaCostShard WUP = Register(ManaAtom.White | ManaAtom.Blue | ManaAtom.OrTwoLife, "W/U/P", "WUP");
    
    
    /* Other */
    public static readonly ManaCostShard X = Register(ManaAtom.IsVariable, "X");

    public static readonly ManaCostShard ColoredX = Register(
        ManaAtom.White |
        ManaAtom.Blue |
        ManaAtom.Black |
        ManaAtom.Red |
        ManaAtom.Green |
        ManaAtom.IsVariable,
        "1"
    );

    public static readonly ManaCostShard Snow = Register(ManaAtom.IsSnow, "S");

    public static readonly ManaCostShard Generic = Register(ManaAtom.Generic, "1");
}
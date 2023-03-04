using SnailHerd.CardForge.Core.Cards;

namespace SnailHerd.CardForge.Core.Common;

public abstract class FlagEnumeration<T> : Enumeration<T>
    where T : FlagEnumeration<T>
{
    protected FlagEnumeration(int id, string name)
        : base(id, name)
    {
    }
}
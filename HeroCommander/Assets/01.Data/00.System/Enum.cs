using System;

public enum Faction
{
    Playerble,
    Enemy
}

public enum WeaponType
{
    Fist,
    Sword,
    Bow,
    Staff
}

[Flags]
public enum Soft_CC : short
{
    None = 0,

    Burns = 1 << 0,
    Slow = 1 << 1,
    IncreaseAttackDelay = 1 << 2,
    DecreaseArmor = 1 << 3,

    All = short.MaxValue
}

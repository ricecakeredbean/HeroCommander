using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct UnitStatTimes
{
    private float startingValue;

    public float XMaxHp;
    public float XDamage;
    public float XSpeed;
    public float XAttackDelay;
    public float XArmor;

    public float SumMaxHp;
    public float SumDamage;
    public float SumSpeed;
    public float SumAttackDelay;
    public float SumArmor;

    public void SetValue()
    {
        XMaxHp = startingValue;
        XDamage = startingValue;
        XSpeed = startingValue;
        XAttackDelay = startingValue;
        XArmor = startingValue;

        SumMaxHp = startingValue;
        SumDamage = startingValue;
        SumSpeed = startingValue;
        SumAttackDelay = startingValue;
        SumArmor = startingValue;
    }

    public UnitStatTimes(float startingValue = 1) : this()
    {
        this.startingValue = startingValue;
        SetValue();
    }

}

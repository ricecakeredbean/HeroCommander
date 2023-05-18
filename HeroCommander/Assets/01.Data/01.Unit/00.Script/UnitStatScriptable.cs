using UnityEngine;
using System;

[CreateAssetMenu(fileName = "UnitStat", menuName = "UnitStat", order = 0)]
public class UnitStatScriptable : ScriptableObject
{
    [SerializeField] private UnitStat stat;
    public UnitStat Stat { get => stat; set => stat = value; }

    [SerializeField] private Faction factionType;
    public Faction FactionType => factionType;

    [SerializeField] private LayerMask layer;
    public LayerMask Layer => layer;

    [SerializeField] private WeaponType weapon;
    public WeaponType Weapon => weapon;
}

[Serializable]
public struct UnitStat
{
    [SerializeField] private float maxHp;
    public float MaxHp { get => maxHp; set => maxHp = value; }

    [SerializeField] private float damage;
    public float Damage { get => damage; set => damage = value; }

    [SerializeField] private float speed;
    public float Speed { get => speed; set => speed = value; }

    [SerializeField] private float range;
    public float Range { get => range; set => range = value; }

    [SerializeField] private float armor;
    public float Armor { get => armor; set => armor = value; }

    [SerializeField] private float attackDelay;
    public float AttackDelay { get => attackDelay; set => attackDelay = value; }
}

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class Unit : MonoBehaviour
{
    private const int zero = 0;

    [SerializeField] private UnitStatScriptable unitInfo;
    public UnitStatScriptable UnitInfo => unitInfo;

    public UnitStatTimes CurrentTimes { get; private set; } = new(1);

    public UnitStat CurrentStat
    {
        get
        {
            UnitStat currentStat = UnitInfo.Stat;

            currentStat.MaxHp *= CurrentTimes.XMaxHp;
            currentStat.MaxHp += CurrentTimes.SumMaxHp;

            currentStat.Damage *= CurrentTimes.XDamage;
            currentStat.Damage += CurrentTimes.SumDamage;

            currentStat.AttackDelay *= CurrentTimes.XAttackDelay;
            currentStat.AttackDelay += CurrentTimes.SumAttackDelay;

            currentStat.Speed *= CurrentTimes.XSpeed;
            currentStat.Speed += CurrentTimes.SumSpeed;

            currentStat.Armor *= CurrentTimes.XArmor;
            currentStat.Armor += CurrentTimes.SumArmor;

            return currentStat;
        }
        private set
        {
        }
    }


    public UnitCondition UnitConditionState { get; private set; }

    private float hp;
    public float Hp => hp;

    private void Start()
    {
        CurrentStat = UnitInfo.Stat;
        UnitConditionState = new(this);
    }

    public virtual void Unit_Hit(float damage)
    {
        damage -= UnitInfo.Stat.Armor;
        hp -= damage;
        if (hp <= zero)
        {
            Unit_Death();
        }
    }

    public void SetStatTimes(UnitStatTimes times)
    {
        CurrentTimes = times;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnitInfo == null)
        {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, UnitInfo.Stat.Range);
    }
#endif
#if UNITY_EDITOR
    private void Update()
    {
        Debug.Log("<color=white>" + UnitConditionState.CurrentStateSoftCC + "</color>");
    }
#endif
#if UNITY_EDITOR
    [ContextMenu("AddBurn")]
    public void AddBurn()
    {
        UnitConditionState.Add_SoftCC(Soft_CC.Burns, 1.0f,1.0f);
    }
#endif
#if UNITY_EDITOR
    [ContextMenu("AddSlow")]
    public void AddSlow()
    {
        UnitConditionState.Add_SoftCC(Soft_CC.Slow, 1.0f, 1.0f);
    }
#endif
#if UNITY_EDITOR
    [ContextMenu("AddIncreseAttackDelay")]
    public void AddIncreseAttackDelay()
    {
        UnitConditionState.Add_SoftCC(Soft_CC.IncreaseAttackDelay, 1.0f, 1.0f);
    }
#endif
#if UNITY_EDITOR
    [ContextMenu("AddDecreaseArmor")]
    public void AddDecreaseArmor()
    {
        UnitConditionState.Add_SoftCC(Soft_CC.DecreaseArmor, 1.0f, 1.0f);
    }
#endif


    public virtual void Unit_Death()
    {
        Destroy(gameObject);
    }
}

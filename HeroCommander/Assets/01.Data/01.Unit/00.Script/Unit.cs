using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CircleCollider2D))]
public class Unit : MonoBehaviour
{
    [SerializeField] private UnitStatScriptable unitInfo;
    public UnitStatScriptable UnitInfo => unitInfo;

    private UnitStat currentStat;
    public UnitStat CurrentStat => currentStat;

    public UnitCondition UnitConditionState { get; private set; } = new();

    [SerializeField] protected WeaponType weapon;
    public WeaponType Weapon => weapon;

    private float hp;
    public float Hp => hp;

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

    private void Start()
    {
        currentStat = UnitInfo.Stat;
    }

#if UNITY_EDITOR
    private void Update()
    {
        Debug.Log("<color=white>" + UnitConditionState.CurrentStateSoftCC + "</color>");
    }
#endif

    public virtual void Unit_Hit(float damage)
    {
        damage -= UnitInfo.Stat.Armor;
        hp -= damage;
        if (hp <= 0)
        {
            Unit_Death();
        }
    }

#if UNITY_EDITOR
    [ContextMenu("AddBurn")]
    public void AddBurn()
    {
        UnitConditionState.Add_SoftCC(Soft_CC.Burns, 1.0f);
    }
#endif
#if UNITY_EDITOR
    [ContextMenu("AddSlow")]
    public void AddSlow()
    {
        UnitConditionState.Add_SoftCC(Soft_CC.Slow, 1.0f);
    }
#endif
#if UNITY_EDITOR
    [ContextMenu("AddIncreseAttackDelay")]
    public void AddIncreseAttackDelay()
    {
        UnitConditionState.Add_SoftCC(Soft_CC.IncreaseAttackDelay, 1.0f);
    }
#endif
#if UNITY_EDITOR
    [ContextMenu("AddDecreaseArmor")]
    public void AddDecreaseArmor()
    {
        UnitConditionState.Add_SoftCC(Soft_CC.DecreaseArmor, 1.0f);
    }
#endif


    public virtual void Unit_Death()
    {
        Destroy(gameObject);
    }
}

public class UnitCondition
{
    private Soft_CC currentStateSoftCC = Soft_CC.None;
    public Soft_CC CurrentStateSoftCC => currentStateSoftCC;

    private Action StateCCAction = null;

    public void Add_SoftCC(Soft_CC add_SoftCC, float time)
    {
        Action currentCCAction = Get_SoftCC_Action(add_SoftCC);
        currentStateSoftCC |= add_SoftCC;
        StateCCAction = currentCCAction;
        var agent = new TimeAgent(time, (agent) => { StateCCAction?.Invoke(); },
            (agent) => { Remove_SoftCC(add_SoftCC); });
        TimerSystem.Instance.Add_TimeAgent(agent);
    }

    public void Remove_SoftCC(Soft_CC remove_SoftCC)
    {
        currentStateSoftCC ^= remove_SoftCC;
        StateCCAction -= Get_SoftCC_Action(remove_SoftCC);
    }

    private void Burns()
    {
#if UNITY_EDITOR
        Debug.Log("<color=red>버닝</color>");
#endif
    }

    private void Slow()
    {
#if UNITY_EDITOR
        Debug.Log("<color=red>슬로우</color>");
#endif
    }

    private void IncereseAttackDelay()
    {
#if UNITY_EDITOR
        Debug.Log("<color=red>공속감소</color>");
#endif
    }

    private void DecreaseArmor()
    {
#if UNITY_EDITOR
        Debug.Log("<color=red>방어력 감소</color>");
#endif
    }

    private Action Get_SoftCC_Action(Soft_CC cc)
    {
        Action SoftCCAction = null;
        switch (cc)
        {
            case Soft_CC.Burns:
                SoftCCAction += Burns;
                break;
            case Soft_CC.Slow:
                SoftCCAction += Slow;
                break;
            case Soft_CC.IncreaseAttackDelay:
                SoftCCAction += IncereseAttackDelay;
                break;
            case Soft_CC.DecreaseArmor:
                SoftCCAction += DecreaseArmor;
                break;
            case Soft_CC.All:
                SoftCCAction += Burns;
                SoftCCAction += Slow;
                SoftCCAction += IncereseAttackDelay;
                SoftCCAction += DecreaseArmor;
                break;
        }
        return SoftCCAction;
    }
}



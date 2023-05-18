using UnityEngine;
using System;
using System.Collections.Generic;

public class UnitCondition
{
    private const int minTimes = 1;

    private Unit Instance;

    private Soft_CC currentStateSoftCC = Soft_CC.None;
    public Soft_CC CurrentStateSoftCC => currentStateSoftCC;


    public void Add_SoftCC(Soft_CC add_SoftCC, float time, float times)
    {
        Soft_CC IsCC = CurrentStateSoftCC & add_SoftCC;
        if (IsCC == add_SoftCC)
        {
#if UNITY_EDITOR
            Debug.Log("<color=red>상태이상이 이미 존재함</color>");
#endif
            return;
        }
        currentStateSoftCC |= add_SoftCC;
        Action<float> currentCCAction = Get_SoftCC_Action(add_SoftCC);
        var agent = new TimeAgent(time, (agent) => { currentCCAction?.Invoke(times); },
            (agent) => { Remove_SoftCC(add_SoftCC); });
        TimerSystem.Instance.Add_TimeAgent(agent);
    }

    public void Remove_SoftCC(Soft_CC remove_SoftCC)
    {
        Get_SoftCC_Action(remove_SoftCC)?.Invoke(minTimes);
        currentStateSoftCC ^= remove_SoftCC;
    }

    private void Burns(float times)
    {
#if UNITY_EDITOR
        Debug.Log($"<color=red>버닝x{times}</color>");
#endif
    }

    private void Slow(float times)
    {
#if UNITY_EDITOR
        Debug.Log($"<color=red>슬로우x{times}</color>");
#endif
    }

    private void IncereseAttackDelay(float times)
    {
#if UNITY_EDITOR
        Debug.Log($"<color=red>공속감소x{times}</color>");
#endif
    }

    private void DecreaseArmor(float times)
    {
#if UNITY_EDITOR
        Debug.Log($"<color=red>방어력 감소x{times}</color>");
#endif
    }

    private Action<float> Get_SoftCC_Action(Soft_CC cc)
    {
        Action<float> SoftCCAction = null;
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

    public UnitCondition(Unit instance)
    {
        Instance = instance;
    }
}
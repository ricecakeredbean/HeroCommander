using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAttackState : UnitState<Knight>
{
    public override void OnEnter(Knight instance, UnitController<Knight> controller)
    {
        base.OnEnter(instance, controller);
#if UNITY_EDITOR
        Debug.Log($"<color=blue>{Instance}АјАн</color>");
#endif
        var agent = new TimeAgent(Instance.CurrentStat.AttackDelay,
        dissableAction: (agent) => { controller.ChangeState(new KnightMoveState()); });
        TimerSystem.Instance.Add_TimeAgent(agent);
    }

    public override void OnExit()
    {
        RaycastHit2D[] hit2Ds = Physics2D.CircleCastAll(Instance.transform.position,
            Instance.CurrentStat.Range, Vector2.one, Instance.CurrentStat.Range, Instance.UnitInfo.Layer);

        foreach (var hit in hit2Ds)
        {
            if (hit.collider.TryGetComponent(out Unit unit))
            {
                if (unit.UnitInfo.FactionType != Instance.UnitInfo.FactionType)
                {
                    unit.Unit_Hit(Instance.CurrentStat.Damage);
                }
            }
        }
    }
}

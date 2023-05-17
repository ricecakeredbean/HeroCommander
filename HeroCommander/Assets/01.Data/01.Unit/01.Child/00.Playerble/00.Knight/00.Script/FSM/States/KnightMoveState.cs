using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMoveState : UnitState<Knight>
{
    public override void OnUpdate()
    {
        if (SearchEnemy())
        {
            Controller.ChangeState(new KnightAttackState());
            return;
        }
        Instance.transform.Translate(Vector2.right * Instance.CurrentStat.Speed * Time.deltaTime);
    }

    private bool SearchEnemy()
    {
        RaycastHit2D[] hit2Ds = Physics2D.CircleCastAll(Instance.transform.position, Instance.CurrentStat.Range, Vector2.up, Instance.CurrentStat.Range);
        foreach (var hit in hit2Ds)
        {
            if (hit.collider.TryGetComponent(out Unit unit))
            {
                if (unit.UnitInfo.FactionType != Instance.UnitInfo.FactionType)
                {
                    return true;
                }
            }
        }
        return false;
    }
}

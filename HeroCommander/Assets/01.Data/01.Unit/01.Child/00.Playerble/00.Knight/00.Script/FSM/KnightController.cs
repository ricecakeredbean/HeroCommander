using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : UnitController<Knight>
{
    protected override UnitState<Knight> IdleState { get; set; } = new KnightMoveState();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : UnitController<Slime>
{
    protected override UnitState<Slime> IdleState { get; set; } = new SlimeMoveState();
}

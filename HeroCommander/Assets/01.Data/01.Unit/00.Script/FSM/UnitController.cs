using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitController<T> : MonoBehaviour where T : Unit
{
    private T instance;

    protected UnitState<T> currentState;
    protected abstract UnitState<T> IdleState { get; set; }

    protected void Awake()
    {
        instance = GetComponent<T>();
    }

    protected void Start()
    {
        ChangeState(IdleState);
        UpdateSystem.Instance.Add_Update(Unit_Action);
    }

    protected void Unit_Action()
    {
        currentState?.OnUpdate();
    }

    public void ChangeState(UnitState<T> nextState)
    {
        currentState?.OnExit();
        currentState = nextState;
        currentState?.OnEnter(instance, this);
    }
}

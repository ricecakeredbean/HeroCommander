using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitController<T> : MonoBehaviour where T : Unit
{
    private T instance;

    protected UnitState<T> currentState;
    protected abstract UnitState<T> IdleState { get; set; }

    private void Awake()
    {
        instance = GetComponent<T>();
    }

    private void Start()
    {
        UpdateSystem.Instance.Add_Update(Unit_Action);
    }

    private void Unit_Action()
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

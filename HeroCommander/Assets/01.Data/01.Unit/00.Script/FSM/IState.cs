using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState<T> where T : Unit
{
    public abstract T Instance { get; set; }
    public abstract UnitController<T> Controller { get; set; }

    public abstract void OnEnter(T instance,UnitController<T> controller);
    public abstract void OnUpdate();
    public abstract void OnExit();
}

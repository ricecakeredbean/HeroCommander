public class UnitState<T> : IState<T> where T : Unit
{
    public T Instance { get; set; }
    public UnitController<T> Controller { get; set; }

    public virtual void OnEnter(T instance, UnitController<T> controller)
    {
        Instance = instance;
        Controller = controller;
    }
    public virtual void OnUpdate()
    {

    }
    public virtual void OnExit()
    {

    }
}

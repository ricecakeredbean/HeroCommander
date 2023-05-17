using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UpdateSystem : MonoSingleTon<UpdateSystem>
{
    private HashSet<Action> updateActionHashSet = new();

    private void Start()
    {
        StartCoroutine(Update_Coroutine());
    }

    IEnumerator Update_Coroutine()
    {
        var fixedUpdate = new WaitForFixedUpdate();
        while (true)
        {
            foreach (var action in updateActionHashSet)
            {
                action?.Invoke();
            }
            yield return fixedUpdate;
        }
    }

    public void Add_Update(Action updateAction)
    {
        updateActionHashSet.Add(updateAction);
    }
}

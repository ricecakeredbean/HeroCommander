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
        while (true)
        {
            foreach (var action in updateActionHashSet)
            {
                action?.Invoke();
            }
            yield return null;
        }
    }
}

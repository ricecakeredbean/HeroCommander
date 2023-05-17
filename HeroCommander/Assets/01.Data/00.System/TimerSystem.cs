using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimerSystem : MonoSingleTon<TimerSystem>
{
    private HashSet<TimeAgent> timeAgentHashSet = new();
    private HashSet<TimeAgent> garbageAgentHashSet = new();

    private void Start()
    {
        UpdateSystem.Instance.Add_Update(TimerUpdate);
    }

    private void TimerUpdate()
    {
        foreach (var agent in timeAgentHashSet)
        {
            agent.updateAction?.Invoke(agent);
            agent.time += Time.fixedDeltaTime;
            if (agent.time >= agent.timerTime)
            {
                agent.dissableAction?.Invoke(agent);
                garbageAgentHashSet.Add(agent);
            }
        }
        foreach (var agent in garbageAgentHashSet)
        {
            timeAgentHashSet.Remove(agent);
        }
    }

    public void Add_TimeAgent(TimeAgent agent)
    {
        timeAgentHashSet.Add(agent);
    }
}

public class TimeAgent
{
    public float timerTime { get; private set; }

    public Action<TimeAgent> updateAction { get; private set; }

    public Action<TimeAgent> dissableAction { get; private set; }

    public float time { get; set; }

    public TimeAgent(float timerTime, Action<TimeAgent> updateAction = null, Action<TimeAgent> dissableAction = null)
    {
        this.timerTime = timerTime;
        this.updateAction = updateAction;
        this.dissableAction = dissableAction;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitStatScriptable unitInfo;
    public UnitStatScriptable UnitInfo => unitInfo;

    private float hp;
    public float Hp => hp;
}

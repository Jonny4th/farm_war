using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UIStatus
{
    public GameObject uiObj;
    public float timeUP = 0.25f;
    public float timeDown = 0.15f;
    public float hold = 1.5f;
    [HideInInspector] public float startScaleX = -1;
    [HideInInspector] public float curr;

    public IEnumerator em;
}

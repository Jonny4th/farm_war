using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Faction<T> : MonoBehaviour
{
    [SerializeField] protected List<T> aliveUnit = new List<T>();
    public List<T> AliveUnit { get { return aliveUnit; } }
    [SerializeField] protected float maxHp = 100;
    public float MaxHp { get { return maxHp; } set { maxHp = value; } }
    [SerializeField] private protected float currentHp;
    public float Hp { get { return (int)currentHp; } }

    [SerializeField] private Transform unitParent;
    public Transform UnitParent { get { return unitParent; } }

    public abstract void TakeDamage(float damage);

    protected virtual void Awake()
    {
        // currentHp = maxHp;
    }
    protected abstract void Start();

    protected void Delay(Action callback, float time) => StartCoroutine(IEDelay(callback, time));
    private IEnumerator IEDelay(Action callback, float time)
    {
        yield return new WaitForSeconds(time);
        callback?.Invoke();
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EmemyFaction : Faction<Farmer>
{



    private event Action<EmemyFaction> updateHp;
    public Action<EmemyFaction> UpdateHp { get { return updateHp; } set { updateHp = value; } }


    public override void TakeDamage(float damage)
    {
        currentHp -= damage;
        // UIManager.instance.UpdateUi(this);
        updateHp?.Invoke(this);
    }
    protected override void Start()
    {
        GameManager.instance.ResetEven += Reset;
        foreach (var T in aliveUnit)
            maxHp += T.MaxHp;
        currentHp = maxHp;
        // Delay(() => UIManager.instance.UpdateUi(this), 1f);
        Delay(() => updateHp?.Invoke(this), 1f);
    }


    private void Reset()
    {
        currentHp = maxHp;
        // Delay(() => UIManager.instance.UpdateUi(this), 1f);
    }
    private void OnDestroy()
    {
        GameManager.instance.ResetEven -= Reset;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmemyFaction : Faction<Farmer>
{
    public override void TakeDamage(float damage)
    {
        currentHp -= damage;
        UIManager.instance.UpdateUi(this);
    }
    protected override void Start()
    {
        GameManager.instance.ResetEven += Reset;
        foreach (var T in aliveUnit)
            maxHp += T.MaxHp;
        currentHp = maxHp;
        Delay(() => UIManager.instance.UpdateUi(this), 1f);
    }


    private void Reset()
    {
        currentHp = maxHp;
        Delay(() => UIManager.instance.UpdateUi(this), 1f);
    }
    private void OnDestroy()
    {
        GameManager.instance.ResetEven -= Reset;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmemyFaction : Faction<EmenTest>
{
    public override void TakeDamage(float damage)
    {
        currentHp -= damage;
        UIManager.instance.UpdateUi(this);
    }
    protected override void Start()
    {
        Delay(() => UIManager.instance.UpdateUi(this), 1f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerFaction : Faction<AnimalTest>
{

    [SerializeField] private float point;
    public float Point { get { return point; } }

    private List<AnimalTest> unitInGrouind = new List<AnimalTest>();
    public List<AnimalTest> UnitInGrouind { get { return unitInGrouind; } }
    [SerializeField] private Transform ratTest;
    public Vector3 RatTest { get { return ratTest.transform.position; } }
    public override void TakeDamage(float damage)
    {
        currentHp -= damage;
        UIManager.instance.UpdateUi(this);

    }

    protected override void Start()
    {
        Delay(() => UIManager.instance.UpdateUi(this), 1f);
    }

    public void Health(float hp)
    {
        currentHp += hp;
        UIManager.instance.UpdateUi(this);
    }


}

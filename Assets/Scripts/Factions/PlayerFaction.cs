using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerFaction : Faction<AnimalTest>
{

    [SerializeField] private float point;
    public float Point { get { return point; } }

    [SerializeField] private List<AnimalTest> unitInGrouind = new List<AnimalTest>();
    public List<AnimalTest> UnitInGrouind { get { return unitInGrouind; } }
    [SerializeField] private Transform ratTest;
    public Vector3 RatTest { get { return ratTest.transform.position; } }
    [SerializeField] private float damage = 1;
    public override void TakeDamage(float damage)
    {
        currentHp -= damage;
        UIManager.instance.UpdateUi(this);
    }

    protected override void Start()
    {
        currentHp = maxHp;
        Delay(() => UIManager.instance.UpdateUi(this), 1f);
    }

    public void Health(float hp)
    {
        currentHp += hp;
        UIManager.instance.UpdateUi(this);
    }
    [SerializeField] private float attackTime = 0.5f;
    private float lastTime = 0;
    private void Update()
    {
        if (UnitInGrouind.Count > 0)
        {
            if (Time.time - lastTime > attackTime)
            {
                lastTime = Time.time;
                GameManager.instance.EmemyFaction.TakeDamage(damage * UnitInGrouind.Count);
            }
        }
    }

}

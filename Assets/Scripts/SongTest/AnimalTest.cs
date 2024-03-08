using FarmWar.Core;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class AnimalTest : MonoBehaviour
{
    public float maxHp = 5;
    public float currentHp;
    [SerializeField] private Node nodeTarget;
    public Node NodeTarget { get { return nodeTarget; } set { nodeTarget = value; } }
    public bool selcetThisUnit = false;
    void Start()
    {
        currentHp = maxHp;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && selcetThisUnit)
        {
            TakeDamage(100);
        }
    }



    void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            // GameManager.instance.PlayerFaction.AliveUnit.Remove(this);
        }
    }
    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
            Dead();
    }
    private void Dead()
    {
        GameManager.instance.PlayerFaction.AnimalDie(this);

    }
    public void Des()
    {
        Destroy(this.gameObject);
    }
    // public void Health(float health)
    // {
    //     currentHp += health;
    //     if (currentHp >= maxHp) currentHp = maxHp;
    //     // GameManager.instance.UpdateHealth();
    //     GameManager.instance.PlayerFaction.UpdateHP();
    // }
}

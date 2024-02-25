using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalTest : MonoBehaviour
{
    public float maxHp = 100;
    public float currentHp;
    [SerializeField] private Node nodeTarget;
    public Node NodeTarget { get { return nodeTarget; } }
    void Start()
    {
        currentHp = maxHp;

        if (GameManager.instance != null)
        {
            // GameManager.instance.PlayerFaction.AliveUnit.Add(this);
            // GameManager.instance.PlayerFaction.UpdateHP();
        }
    }


    void Update()
    {
        if (currentHp <= 0)
            Destroy(this.gameObject);
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
        // GameManager.instance.UpdateHealth();
        // GameManager.instance.PlayerFaction.UpdateHP();
    }
    // public void Health(float health)
    // {
    //     currentHp += health;
    //     if (currentHp >= maxHp) currentHp = maxHp;
    //     // GameManager.instance.UpdateHealth();
    //     GameManager.instance.PlayerFaction.UpdateHP();
    // }
}

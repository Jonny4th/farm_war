using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalTest : MonoBehaviour
{
    public float maxHp = 100;
    public float currentHp;
    void Start()
    {
        currentHp = maxHp;

        if (GameManager.instance != null)
        {
            GameManager.instance.AddAliveAnimal(this);
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
            GameManager.instance.RemoveAnimal(this);
        }
    }
    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        GameManager.instance.UpdateHealth();
    }
    public void Health(float health)
    {
        currentHp += health;
        if (currentHp >= maxHp) currentHp = maxHp;
        GameManager.instance.UpdateHealth();
    }
}

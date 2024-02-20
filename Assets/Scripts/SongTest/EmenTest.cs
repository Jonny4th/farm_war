using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmenTest : MonoBehaviour
{
    public float maxHp = 100;
    public float currentHp;
    void Start()
    {
        currentHp = maxHp;

        if (GameManager.instance != null)
        {
            // GameManager.instance.AddAliveEmemy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHp <= 0)
            Destroy(this.gameObject);
    }
    void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            // GameManager.instance.RemoveEmemy(this);
        }
    }
    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        // GameManager.instance.UpdateEmenyHealth();
    }
}

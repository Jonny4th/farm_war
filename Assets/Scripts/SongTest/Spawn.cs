using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    void Start()
    {

    }
    private void Animal()
    {
        GameObject Obj = new GameObject("Animal", typeof(AnimalTest));
        //GameManager.instance.PlayerFaction.AliveUnit.Add(Obj.GetComponent<AnimalTest>());
    }
    private void Ememy()
    {
        GameObject Obj = new GameObject("Emem", typeof(EmenTest));
        // GameManager.instance.EmemyFaction.AliveUnit.Add(Obj.GetComponent<EmenTest>());
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.State != GameState.Action) return;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Animal();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Ememy();
        }


        if (Input.GetKeyDown(KeyCode.O))
        {
            GameManager.instance.PlayerFaction.TakeDamage(200);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameManager.instance.EmemyFaction.TakeDamage(200);
        }

    }

}

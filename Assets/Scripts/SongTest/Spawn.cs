using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    void Start()
    {
        Animal();
        Ememy();
    }
    private void Animal()
    {
        GameObject gameObject = new GameObject("Animal");
        gameObject.AddComponent<AnimalTest>();
    }
    private void Ememy()
    {
        GameObject gameObject = new GameObject("Emem");
        gameObject.AddComponent<EmenTest>();
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


        if (Input.GetKeyDown(KeyCode.H))
        {
            int r = Random.Range(0, GameManager.instance.AliveAnimal.Count - 1);
            GameManager.instance.AliveAnimal[r].Health(50);
        }


        if (Input.GetKeyDown(KeyCode.J))
        {
            // GameManager.instance.CurrentPoint += 10;
            GameManager.instance.UpdatePoint(-10);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            int r = Random.Range(0, GameManager.instance.AliveAnimal.Count - 1);
            GameManager.instance.AliveAnimal[r].TakeDamage(20);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            int r = Random.Range(0, GameManager.instance.AliveEmemy.Count - 1);
            GameManager.instance.AliveEmemy[0].TakeDamage(50);
        }
    }
}

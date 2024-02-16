using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planting : MonoBehaviour
{
    [SerializeField] private GameObject[] crops;
    [SerializeField] private bool planted;
    // Start is called before the first frame update
    void Start()
    {
        planted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && planted == false)
        {
            planted = true;
            PlantTheCrop();
        }

    }

    private void PlantTheCrop()
    {
        Instantiate(crops[0], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2, gameObject.transform.position.z), Quaternion.identity);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Crop"))
        {
            planted = false;
        }
    }

}
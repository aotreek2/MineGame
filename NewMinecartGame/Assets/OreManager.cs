using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreManager : MonoBehaviour
{
    public int oreHealth;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "goldOre")
        {
            oreHealth = Random.Range(5, 10);
        }
        else if (gameObject.tag == "diamondOre")
        {
            oreHealth = Random.Range(1, 3);
        }
        else if (gameObject.tag == "invineOre")
        {
            oreHealth = Random.Range(3, 8);
        }
        else
        {
            Debug.Log("Something went wrong.");
        }
    }
}

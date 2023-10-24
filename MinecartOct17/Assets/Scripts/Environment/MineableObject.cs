using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineableObject : MonoBehaviour
{
    public int objectHealth;

    public bool ore = false;
    public bool enemy = false;

    public PickaxeManager pickaxeManager;

    // Start is called before the first frame update
    void Start()
    {
        if (ore == true)
        {
            if (gameObject.tag == "goldOre")
            {
                objectHealth = Random.Range(5, 10);
            }
            else if (gameObject.tag == "diamondOre")
            {
                objectHealth = 1;
            }
            else if (gameObject.tag == "invineOre")
            {
                objectHealth = Random.Range(3, 8);
            }
            else if (gameObject.tag == "rock")
            {
                objectHealth = Random.Range(7, 10);
            }
            else
            {
                Debug.Log("Something went wrong.");
            }
        }
        else if (enemy == true)
        {
            if (gameObject.tag == "Zombie")
            {
                
            }
            else if (gameObject.tag == "Bat")
            {

            }
        }
        else
        {

        }
    }

    //Someone clicked the object.
    private void OnMouseDown()
    {
        if (pickaxeManager.active && (Mathf.Abs(transform.parent.transform.position.x - pickaxeManager.transform.position.x) <= 2.5))
        {
            pickaxeManager.SwingPickaxe(gameObject);
        }
    }

}

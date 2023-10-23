using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PickaxeManager : MonoBehaviour
{
    public TextMeshProUGUI oreStats;
    public int gold = 0;
    public int diamond = 0;
    public int invine = 0;

    public AudioSource mining;
    public AudioSource diamondTing;

    // Ore ping thing
    public Animator orePing;

    public bool active = false;


    // Start is called before the first frame update
    void Start()
    {
        //orePing = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            transform.GetComponent<Animator>().Play("PickaxeSwing");
        }
    }

    public void SwingPickaxe(GameObject minedObject)
    {
        if (active == true)
        {
            if (minedObject.tag == "goldOre" || minedObject.tag == "invineOre" || minedObject.tag == "diamondOre" || minedObject.tag == "rock")
            {
                //Plays sound and makes sparks
                mining.pitch = Random.Range(1f, 1.8f);
                mining.Play();
                minedObject.transform.parent.transform.GetChild(1).transform.GetComponent<ParticleSystem>().Play();

                //Adds correct ore
                switch (minedObject.tag)
                {
                    case "goldOre":
                        gold++;
                        break;
                    case "invineOre":
                        invine++;
                        break;
                    case "diamondOre":
                        diamond++;
                        diamondTing.Play();
                        break;
                    default:
                        break;
                }

                //Damages ore and checks if it should be destroyed
                minedObject.GetComponent<MineableObject>().objectHealth -= 1;
                if (minedObject.GetComponent<MineableObject>().objectHealth <= 0)
                {
                    Destroy(minedObject);
                    minedObject.transform.parent.transform.GetChild(2).transform.GetComponent<ParticleSystem>().Play();
                }

                //Updates UI
                UpdateOres();
            }
            else if (minedObject.tag == "Zombie")
            {
                mining.pitch = Random.Range(1f, 1.8f);
                mining.Play();

                minedObject.GetComponent<Zombie>().TakeDamage(2);
            }
            else if (minedObject.tag == "bat")
            {
                mining.pitch = Random.Range(1f, 1.8f);
                mining.Play();

                minedObject.GetComponent<Bat>().TakeDamage(2);
            }
        }
        else
        {
            Debug.Log("Pickaxe not active");
        }
    }

    public void UpdateOres()
    {
        oreStats.text = $"Gold: {gold} / Invine: {invine} / Diamond: {diamond}";
    }
}
